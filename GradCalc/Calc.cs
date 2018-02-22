using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradCalc
{
    class Calc
    {
        public static string CalculateGraduation(Noten grades)
        {
            Dictionary<string, Course> translate = new Dictionary<string, Course>
            {
                ["De"] = Course.German,
                ["En"] = Course.English,
                ["Ma"] = Course.Maths,
                ["Bio"] = Course.Biology,
                ["Ch"] = Course.Chemistry,
                ["Ph"] = Course.Physics,
                ["WP1"] = Course.WP1,
                ["WP2"] = Course.WP2,
                ["LER"] = Course.LER,
                ["Sp"] = Course.PE,
                ["Mu"] = Course.Music,
                ["Ku"] = Course.Art,
                ["WAT"] = Course.WAT,
                ["Ge"] = Course.History,
                ["Pb"] = Course.Politics,
                ["Geo"] = Course.Geography,
            };

            Specialization sp(string s)
            {
                switch (s.ToUpper())
                {
                    case "A": return Specialization.A;
                    case "B": return Specialization.B;
                    default: return Specialization.None;
                }
            }

            string[] fächer = new string[] {"De", "En", "Ma", "Bio", "Ch", "Ph", "WP1", "WP2", "LER", "Sp", "Mu", "Ku", "WAT", "Ge", "Pb", "Geo" };
            List<Grade> newGrades = new List<Grade>();
            for (int i = 0; i < grades.Length; i++)
                newGrades.Add(new Grade(translate[fächer.Single(s => grades.Fach(s) == i)], sp(grades.Kurs(i)), grades.Zensur(i)));
            return CalculateGraduation(newGrades);
        }

        public static string CalculateGraduation(IEnumerable<Grade> grades)
        {
            if (FORQ(grades))
                return "FORQ";
            if (FOR(grades))
                return "FOR";
            if (EBR(grades))
                return "EBR";
            return "BBR";
        }

        private static bool EBR(IEnumerable<Grade> grades)
        {
            var correctedGrades = grades.Select(g => g.Specialization == Specialization.B ? g.With(mark: g.Mark - 1) : g).ToArray();
            bool compensate()
            {
                var fail = correctedGrades.First(g => g.Mark == 5);
                var compensation = correctedGrades.FirstOrDefault(g => g.Mark <= 3 && g.CourseGroup == fail.CourseGroup);
                if (compensation is null)
                    return false;
                correctedGrades[Array.IndexOf(correctedGrades, fail)] = fail.With(mark: 4);
                correctedGrades[Array.IndexOf(correctedGrades, compensation)] = compensation.With(mark: 4);
                return true;
            }

            if (correctedGrades.Any(g => g.Mark == 6))
                return false;
            if (correctedGrades.Count(g => g.Course == Course.German && g.Mark > 4 || g.Course == Course.Maths && g.Mark > 4) > 1)
                return false;

            switch (correctedGrades.Count(g => g.Mark == 5))
            {
                case 0: break;
                case 1:
                    if (compensate())
                        break;
                    goto default;
                case 2:
                    if (compensate())
                        goto case 1;
                    goto default;
                default: return false;
            }

            return true;
        }

        private static bool FOR(IEnumerable<Grade> grades) => AnyFOR(grades, 2, 4, 3, 3, 2, 4);

        private static bool FORQ(IEnumerable<Grade> grades) => AnyFOR(grades, 3, 3, 2, 2, 1, 3);

        private static bool AnyFOR(IEnumerable<Grade> grades, int bCourseMinCount, int bWorstMark, int aWorstMark, int minRestMarks, int restMaxFives, int restMaxAverage)
        {
            /// <param name="aCourseGrade">for a compensation, have at least this grade in an A-Course</param>
            /// <param name="otherGrade">for a compensation, have at least this grade in any other course</param>
            bool isCompensated(int aCourseGrade, int otherGrade) =>
                grades.Any(g => g.Specialization == Specialization.A && g.Mark <= aCourseGrade || (g.Specialization == Specialization.B || g.Course == Course.WP1) && g.Mark <= otherGrade);
            Grade[] special(Specialization s) => grades.Where(g => g.Specialization == s).ToArray();
            var bCourses = special(Specialization.B);
            var aCourses = special(Specialization.A);
            var rest = special(Specialization.None);
            IEnumerable<Grade> coursesAbove(int b, int a) => bCourses.Where(g => g.Mark > b).Concat(aCourses.Where(g => g.Mark > a));


            if (bCourses.Length < bCourseMinCount)
                return false;

            if (coursesAbove(bWorstMark + 1, aWorstMark + 1).Any())
                return false;

            switch (coursesAbove(bWorstMark, aWorstMark).Count())
            {
                case 0: break;
                case 1:
                    if (isCompensated(aWorstMark - 1, bWorstMark - 1))
                        break;
                    goto default;
                default: return false;
            }

            if (rest.Count(g => g.Mark <= minRestMarks) < 2)
                return false;

            if (rest.Any(g => g.Mark == 6))
                return false;

            if (rest.Count(g => g.Mark == 5) > restMaxFives)
                return false;

            if (rest.Average(g => g.Mark) > restMaxAverage)
                return false;

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradCalc
{
    enum Course
    {
        German,
        English,
        Maths,
        Biology,
        Chemistry,
        Physics,
        WP1,
        WP2,
        LER,
        PE,
        Music,
        Art,
        WAT,
        History,
        Politics,
        Geography,
    }

    enum Specialization { None, B, A }

    enum CourseGroup { None, One, Two }

    class Grade
    {
        public Course Course { get; }
        public Specialization Specialization { get; }
        public int Mark { get; }

        public CourseGroup CourseGroup
        {
            get
            {
                switch (Course)
                {
                    case Course.German:
                    case Course.Maths:
                    case Course.English:
                    case Course.WP1: return CourseGroup.One;
                    default: return CourseGroup.Two;
                }
            }
        }

        public Grade(Course course, Specialization specialization, int mark)
        {
            Course = course;
            Specialization = specialization;
            Mark = mark;
        }

        public Grade With(Course? course = null, Specialization? specialization = null, int? mark = null)
        {
            return new Grade(course ?? Course, specialization ?? Specialization, mark ?? Mark);
        }

        public override string ToString() => $"{Mark} {Course} {Specialization}";
    }
}

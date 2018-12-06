using System;
using System.Collections.Generic;
using System.Text;

namespace AscrModelingMatrixesClassLibrary
{
    /// <summary>
    /// СЛАУ
    /// </summary>
    [Serializable]
    public class Slae
    {
        public MatrixCSR Matrix { get; set; }
        public double[] RightSide { get; set; }

        /// <summary>
        /// Массив правильных коэффициентов
        /// </summary>
        public double[] X { get; set; }

        #region Конструкторы
        /// <summary>
        /// Конструктор для сериализации
        /// </summary>
        public Slae()
        {
        }

        public Slae(int n, int numDiagonals, double maxValueOfMatrix, double maxValueOfRightSide)
        {
            Matrix = new MatrixCSR(n, numDiagonals);

            RightSide = new double[n];
            //FillArrayRnd(RightSide, maxValueOfRightSide);

            X = new double[n];
        }

        #endregion
    }
}

using MinimalSquares.Math;

namespace Test1
{
    public class UnitTest1
    {
        [Fact]
        public void EMatrixTest()
        {
            SigmaMatrix matrix = SigmaMatrix.GetE(5);
            Assert.True(matrix.GetDeterminant() == 1.0f, "����������� ����� ��������� ������� - 1");
        }

        [Fact]
        public void DetCustomTest()
        {
            float[,] values = new float[3, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };

            SigmaMatrix matrix = new SigmaMatrix(values);
            Assert.True(matrix.GetDeterminant() == 0.0f, "����������� ���� ������� - 0");
        }

        [Fact]
        public void DetCustomTest2()
        {
            float[,] values = new float[3, 3]
            {
                { 1, 2, 3 },
                { 4, 10, 6 },
                { 7, 8, 9 }
            };

            SigmaMatrix matrix = new SigmaMatrix(values);
            Assert.True(matrix.GetDeterminant() == -60f, "����������� ���� ������� - -60");
        }

        [Fact]
        public void MinorCustomTest()
        {
            float[,] values = new float[3, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };

            SigmaMatrix matrix = new SigmaMatrix(values).GetMinor(0, 0);
            Assert.True(matrix.GetDeterminant() == -3f, "����������� ������ ������ ���� ����� -3");
        }

        [Fact]
        public void ReplaceColumnCustomTest()
        {
            float[,] values = new float[3, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };

            SigmaMatrix matrix = new SigmaMatrix(values).ReplaceColumn(new float[3] { 2, 5, 8}, 0);
            Assert.True(matrix.GetDeterminant() == 0, "����������� ���� ������� ������ ���� ����� -0");
        }
    }
}
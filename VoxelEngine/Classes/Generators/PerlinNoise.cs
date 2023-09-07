using System;

namespace VoxelEngine.Classes.Generators
{
    class PerlinNoise
    {
        private int seed;

        public PerlinNoise(int seed)
        {
            this.seed = seed;
        }

        public double GetNoise(int x, int y)
        {
            double total = 0.0;
            double frequency = 1.0;
            double amplitude = 1.0;
            double maxValue = 0.0;

            for (int i = 0; i < 4; i++)
            {
                total += InterpolatedNoise(x * frequency, y * frequency) * amplitude;
                maxValue += amplitude;
                amplitude *= 0.5;
                frequency *= 2;
            }

            return total / maxValue;
        }

        private double Noise(int x, int y)
        {
            int n = x + y * 57 + seed;
            n = (n << 13) ^ n;
            return (1.0 - ((n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0);
        }

        private double SmoothedNoise(int x, int y)
        {
            double corners = (Noise(x - 1, y - 1) + Noise(x + 1, y - 1) + Noise(x - 1, y + 1) + Noise(x + 1, y + 1)) / 16.0;
            double sides = (Noise(x - 1, y) + Noise(x + 1, y) + Noise(x, y - 1) + Noise(x, y + 1)) / 8.0;
            double center = Noise(x, y) / 4.0;
            return corners + sides + center;
        }

        private double InterpolatedNoise(double x, double y)
        {
            int integer_X = (int)x;
            double fractional_X = x - integer_X;

            int integer_Y = (int)y;
            double fractional_Y = y - integer_Y;

            double v1 = SmoothedNoise(integer_X, integer_Y);
            double v2 = SmoothedNoise(integer_X + 1, integer_Y);
            double v3 = SmoothedNoise(integer_X, integer_Y + 1);
            double v4 = SmoothedNoise(integer_X + 1, integer_Y + 1);

            double i1 = Interpolate(v1, v2, fractional_X);
            double i2 = Interpolate(v3, v4, fractional_X);

            return Interpolate(i1, i2, fractional_Y);
        }

        private double Interpolate(double a, double b, double x)
        {
            double ft = x * Math.PI;
            double f = (1.0 - Math.Cos(ft)) * 0.5;
            return a * (1.0 - f) + b * f;
        }
    }
}

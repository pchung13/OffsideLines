using OffsideLines.Core.Geometry;

namespace OffsideLines.Tests;

public class VanishingPointCalculatorTests
{
    [Fact]
    public void PerpendicularLines_IntersectAtOrigin()
    {
        var h = new LineSegment(-10, 0, 10, 0);  // horizontal through origin
        var v = new LineSegment(0, -10, 0, 10);  // vertical through origin
        var vp = VanishingPointCalculator.Compute(h, v);

        Assert.NotNull(vp);
        Assert.Equal(0, vp!.X, precision: 6);
        Assert.Equal(0, vp.Y, precision: 6);
    }

    [Fact]
    public void ConvergingLines_IntersectAtVanishingPoint()
    {
        // Two lines that converge at (500, 200) — typical perspective horizon
        var l1 = new LineSegment(0, 400, 500, 200);
        var l2 = new LineSegment(1000, 400, 500, 200);
        var vp = VanishingPointCalculator.Compute(l1, l2);

        Assert.NotNull(vp);
        Assert.Equal(500, vp!.X, precision: 4);
        Assert.Equal(200, vp.Y, precision: 4);
    }

    [Fact]
    public void ParallelLines_ReturnsNull()
    {
        var l1 = new LineSegment(0, 100, 800, 100); // horizontal
        var l2 = new LineSegment(0, 200, 800, 200); // parallel horizontal
        var vp = VanishingPointCalculator.Compute(l1, l2);

        Assert.Null(vp);
    }

    [Fact]
    public void DiagonalLines_IntersectCorrectly()
    {
        // y = x  (from 0,0 to 100,100)  and  y = -x + 200 (from 0,200 to 100,100)
        var l1 = new LineSegment(0, 0, 100, 100);
        var l2 = new LineSegment(0, 200, 100, 100);
        var vp = VanishingPointCalculator.Compute(l1, l2);

        Assert.NotNull(vp);
        Assert.Equal(100, vp!.X, precision: 4);
        Assert.Equal(100, vp.Y, precision: 4);
    }
}

namespace OffsideLines.Core.Geometry;

/// <summary>
/// Computes the vanishing point of two line segments.
/// The vanishing point is where two lines (parallel in reality) meet in perspective.
/// When the lines are truly parallel in image space (no perspective), returns null.
/// </summary>
public static class VanishingPointCalculator
{
    /// <summary>
    /// Returns the intersection of the infinite lines through <paramref name="line1"/>
    /// and <paramref name="line2"/>, or null if they are parallel.
    /// </summary>
    public static Point2D? Compute(LineSegment line1, LineSegment line2)
    {
        // Express each line in the form a*x + b*y = c
        double a1 = line1.Y2 - line1.Y1;
        double b1 = line1.X1 - line1.X2;
        double c1 = a1 * line1.X1 + b1 * line1.Y1;

        double a2 = line2.Y2 - line2.Y1;
        double b2 = line2.X1 - line2.X2;
        double c2 = a2 * line2.X1 + b2 * line2.Y1;

        double det = a1 * b2 - a2 * b1;
        if (Math.Abs(det) < 1e-10)
            return null; // Lines are parallel — VP at infinity

        return new Point2D(
            (c1 * b2 - c2 * b1) / det,
            (a1 * c2 - a2 * c1) / det
        );
    }
}

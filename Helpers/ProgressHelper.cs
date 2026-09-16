namespace PracticeVault.Helpers
{
    public class ProgressHelper
    {
        public string GetProgressLabel(int progress)
        {
            if (progress >= 100)
            {
                return "Complete";
            }

            if (progress >= 50)
            {
                return "In Progress";
            }

            return "Just Started";
        }
    }
}
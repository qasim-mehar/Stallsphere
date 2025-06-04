
namespace AboutLibrary
{   public class AboutInfo
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Mission { get; set; }
        public string Vision { get; set; } 
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; } 
    }
        public static class About
        {
            public static List<AboutInfo> GetFaqs()
            {
                return new List<AboutInfo>
            {
                 new AboutInfo
                {
                    Title = "About TechSolutions Inc.",
Description = "Founded in 2010, TechSolutions Inc. has been at the forefront of innovative software development, helping businesses transform their operations through cutting-edge technology solutions.",
Mission = "To empower organizations with intuitive, scalable software solutions that drive efficiency and growth.",
Vision = "A world where technology seamlessly enhances every business operation, creating opportunities for innovation and connection.",
ContactEmail = "info@techsolutions.com",
ContactPhone = "(555) 123-4567",
                }
                 
            };
            }
        }
    
}

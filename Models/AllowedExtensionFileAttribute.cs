namespace Tranning.Models
{
    internal class AllowedExtensionFileAttribute : Attribute
    {
        private string[] strings;

        public AllowedExtensionFileAttribute(string[] strings)
        {
            this.strings = strings;
        }
    }
}
namespace Pusharp.Net.DTO
{
    public class IconDTO
    {
        public string Url { get; set; }
        public override string ToString()
        {
            return this.Url;
        }
    }
}
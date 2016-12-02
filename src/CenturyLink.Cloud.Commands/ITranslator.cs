namespace CenturyLink.Cloud.Commands
{
    public interface ITranslator<Source, Destination>
        where Source : class
        where Destination : class
    {
        Destination Translate(Source source);
    }
}

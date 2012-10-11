namespace Crystalbyte.Spectre.Web{
    public interface IDataProvider{
        bool OnRequestProcessing(Request request);
        void OnDataBlockReading(DataBlockReadingEventArgs e);
        void OnResponseHeadersReading(ResponseHeadersReadingEventArgs e);
    }
}
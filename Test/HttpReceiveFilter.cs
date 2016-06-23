using SuperSocket.ProtoBase;
using System;

namespace SuperSocket.ClientEngine.Test
{
    public class HttpReceiveFilter : HttpHeaderReceiveFilterBase<HttpPackageInfo>
    {
        protected override IReceiveFilter<HttpPackageInfo> GetBodyReceiveFilter(HttpHeaderInfo header, int headerSize)
        {
            var contentLength = 0;
            var strContentLength = header.Get("Content-Length");

            if (string.IsNullOrEmpty(strContentLength))
                contentLength = -1;
            else
                contentLength = int.Parse(strContentLength);

            var totalLength = contentLength < 0 ? int.MaxValue : headerSize + contentLength;
            return new HttpBodyReceiveFilter(header, totalLength, headerSize);
        }

        protected override HttpPackageInfo ResolveHttpPackageWithoutBody(HttpHeaderInfo header)
        {
            return new HttpPackageInfo("Test", header, string.Empty);
        }
    }
}

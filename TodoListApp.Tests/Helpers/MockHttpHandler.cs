using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace TodoListApp.Tests.Helpers;

public class MockHttpHandler<T>
{
    //success
    internal static Mock<HttpMessageHandler> SetupGetRequest(List<T> response)
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(response))
        };

        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>(
        "SendAsync",
        ItExpr.IsAny<HttpRequestMessage>(),
        ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return mockHandler;

    }
    
    //not found
    internal static Mock<HttpMessageHandler> SetupReturnNotFound()
    {
        var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent("")
        };

        mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        
        var mockHandler = new Mock<HttpMessageHandler>();
        mockHandler.Protected().Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        return mockHandler;
    }
}
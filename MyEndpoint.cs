using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SeperateConcernsLogger.Utilities;
using Serilog;
using Serilog.Context;

namespace SeperateConcernsLogger;

public class MyEndpoint : Endpoint<MyRequest>
{
    public override void Configure()
    {
        Post("/api/user/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(MyRequest req, CancellationToken ct)
    {
        var response = new MyResponse()
        {
            FullName = req.FirstName + " " + req.LastName,
            IsOver18 = req.Age > 18
        };

        ///Create islemleride kullanilir
        WLogger.Create(existing_data: req, userId: req.UserId);

        ///Create islemlerinde hata oldugu zaman kullanilir
        WLogger.Create(existing_data: req, userId: req.UserId, errorMessage: "");

        ///Update ve delete islemleri icin de ayni mantikla kullanilir.

        await SendAsync(response);
    }
}



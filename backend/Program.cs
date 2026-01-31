using unit_converter;
class Program
{
    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    /// <remarks>
    /// This program will create a web application that exposes endpoints
    /// for converting length, weight, and temperature measurements between
    /// different units.
    /// </remarks>
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    public static void Main(String[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // TODO: Substitute hard-coded client origin with safer option (e.g. `.env`)
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors();


        /*
ENDPOINTS:
- Will mainly be POST commands (send a form)
- Length Form, Weight Form, Temp Form
    - Three Components of Each form:
        1. Measurement Size
        2. Units (Convert From)
        3. Units (Convert TO)
    Button at the Bottom <CONVERT>

- Need handlers for each conversion method.
URL Should be "/{base-url}-<measurement_type>_form
*/
        app.UseHttpsRedirection();

        app.MapPost("/convert/length", (ConversionRequest req) =>
        {
            LengthConversion.LengthUnits parsedFromUnit = LengthConversion.ParseUnit(req.FromUnit);
            LengthConversion.LengthUnits parsedToUnit = LengthConversion.ParseUnit(req.ToUnit);

            double convertedLength = LengthConversion.ConvertLength(req.Value, parsedFromUnit, parsedToUnit);

            ConversionResponse res = new()
            {
                Result = convertedLength,
                OriginalValue = req.Value,
                FromUnit = req.FromUnit,
                ToUnit = req.ToUnit,
                FromUnitSymbol = LengthConversion.GetUnitSymbol(parsedFromUnit),
                ToUnitSymbol = LengthConversion.GetUnitSymbol(parsedToUnit),
            };
            return res;
        });

        app.MapPost("/convert/weight", (ConversionRequest req) =>
        {
            WeightConversion.WeightUnits parsedFromUnit = WeightConversion.ParseUnit(req.FromUnit);
            WeightConversion.WeightUnits parsedToUnit = WeightConversion.ParseUnit(req.ToUnit);

            double convertedWeight = WeightConversion.ConvertWeight(req.Value, parsedFromUnit, parsedToUnit);

            ConversionResponse res = new()
            {
                Result = convertedWeight,
                OriginalValue = req.Value,
                FromUnit = req.FromUnit,
                ToUnit = req.ToUnit,
                FromUnitSymbol = WeightConversion.GetUnitSymbol(parsedFromUnit),
                ToUnitSymbol = WeightConversion.GetUnitSymbol(parsedToUnit),
            };

            Console.WriteLine(res);
            return res;
        });

        app.MapPost("/convert/temperature", (ConversionRequest req) =>
        {
            TemperatureConversion.TemperatureUnits parsedFromUnit = TemperatureConversion.ParseUnit(req.FromUnit);
            TemperatureConversion.TemperatureUnits parsedToUnit = TemperatureConversion.ParseUnit(req.ToUnit);

            double convertedTemp = TemperatureConversion.ConvertTemperature(req.Value, parsedFromUnit, parsedToUnit);

            ConversionResponse res = new()
            {
                Result = convertedTemp,
                OriginalValue = req.Value,
                FromUnit = req.FromUnit,
                ToUnit = req.ToUnit,
                FromUnitSymbol = TemperatureConversion.GetUnitSymbol(parsedFromUnit),
                ToUnitSymbol = TemperatureConversion.GetUnitSymbol(parsedToUnit),
            };
            Console.WriteLine(res);
            return res;
        });

        app.MapGet("/convert/length/units", () =>
        {
            return LengthConversion.GetAllUnits();
        });

        app.MapGet("/convert/weight/units", () =>
        {
            return WeightConversion.GetAllUnits();
        });

        app.MapGet("/convert/temperature/units", () =>
        {
            return TemperatureConversion.GetAllUnits();
        });

        app.Run();
    }
}

# Sample to provoke "bug" in Microsoft.AspNetCore.Mvc.NewtonsoftJson

This reproduces the issue where and error is thrown when formatting input if the given input has a byte order mark (BOM). In the given library  the nuget package `Microsoft.AspNetCore.Mvc.NewtonsoftJson` has been added, and has been hooked up to be used in

```csharp
// Use AddNewtonsoftJson instead of SystemTextJson
builder.Services.AddControllers()
    .AddNewtonsoftJson();
```

The given Test sample generates a json stream using utf8 encoding that has a byte order mark in the start, and results in status code 400 because the given input cannot be deserialized. In the api log the following is logged

```
dbug: Microsoft.AspNetCore.Mvc.Formatters.NewtonsoftJsonInputFormatter[1]
      JSON input formatter threw an exception.
      Newtonsoft.Json.JsonReaderException: Unexpected character encountered while parsing value: ?. Path '', line 0, position 0.
         at Newtonsoft.Json.JsonTextReader.ParseValue()
         at Newtonsoft.Json.JsonTextReader.Read()
         at Newtonsoft.Json.JsonReader.ReadAndMoveToContent()
         at Newtonsoft.Json.JsonReader.ReadForType(JsonContract contract, Boolean hasConverter)
         at Newtonsoft.Json.Serialization.JsonSerializerInternalReader.Deserialize(JsonReader reader, Type objectType, Boolean checkAdditionalContent)

```

This occurs because the Newtonsoft can not handle the byte order mark. It's not a bug in Newtonsoft. But the issue is that if the build in SystemTextJsonFormatter is used, it will handle the passed json with a byte order mark and not throw an exception. And also the .net framework version of asp.net did also handle it.

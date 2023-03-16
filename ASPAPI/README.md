# 問題解決
## .NET6使用Incloude()時出現報錯
請在Program.cs中添加以下設置
```c#
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => 
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

```

# 優化及待辦事項
## 1.把各個Repository的CRUD命名統一
## 2.使用驗證插件時，如何將關聯表也一起驗證
## 3.把User Table加進資料庫中
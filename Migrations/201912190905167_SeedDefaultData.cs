namespace NutritionWatcher.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    
    public partial class SeedDefaultData : DbMigration
    {
        List<string> Foods { get; set; } = new List<string>();

        void ReadFoods()
        {
            using (StreamReader reader = new StreamReader(new FileStream("../App_Data/SqlValues.txt", FileMode.Open), System.Text.Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                    Foods.Add(reader.ReadLine());

                reader.Close();
            }
        }

        public override void Up()
        {
            Sql("INSERT INTO [dbo].[PermissionModels] ([Name]) VALUES (N'admin'), (N'member');");

            Sql("INSERT INTO [dbo].[StyleModels] ([Name]) VALUES (N'light'), (N'dark');");

            Sql("INSERT INTO [dbo].[UserModels] ([Username], [Firstname], [Lastname], " +
                "[Password], [Email], [Permission_Id], [Style_Id]) VALUES " +
                "(N'kliper', N'Patrik', N'Egri', N'-1418187776', N'egripatrik@gmail.com', " + 
                "(SELECT [Id] FROM [dbo].[PermissionModels] WHERE [Name] LIKE N'admin'), " +
                "(SELECT [Id] FROM [dbo].[StyleModels] WHERE [Name] LIKE N'dark'));");

            ReadFoods();
            foreach (string x in Foods)
            {
                Sql($"INSERT INTO [dbo].[FoodModels] ([Name], [Protein], [Fat], [Hydrocarbonate], [Gramm]) VALUES {x.Replace("),", ");")}");
            }
        }
        
        public override void Down()
        {
        }
    }
}

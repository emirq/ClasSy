namespace ClasSy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfessorIdToGradeModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "ProfessorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Grades", "ProfessorId");
            AddForeignKey("dbo.Grades", "ProfessorId", "dbo.Professors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "ProfessorId", "dbo.Professors");
            DropIndex("dbo.Grades", new[] { "ProfessorId" });
            DropColumn("dbo.Grades", "ProfessorId");
        }
    }
}

namespace ClasSy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdNamesFixes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Grades", new[] { "Student_Id" });
            DropIndex("dbo.SchoolClasses", new[] { "Professor_Id" });
            DropColumn("dbo.Grades", "StudentId");
            DropColumn("dbo.SchoolClasses", "ProfessorId");
            RenameColumn(table: "dbo.Grades", name: "Student_Id", newName: "StudentId");
            RenameColumn(table: "dbo.SchoolClasses", name: "Professor_Id", newName: "ProfessorId");
            AlterColumn("dbo.Grades", "StudentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.SchoolClasses", "ProfessorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Grades", "StudentId");
            CreateIndex("dbo.SchoolClasses", "ProfessorId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SchoolClasses", new[] { "ProfessorId" });
            DropIndex("dbo.Grades", new[] { "StudentId" });
            AlterColumn("dbo.SchoolClasses", "ProfessorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Grades", "StudentId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.SchoolClasses", name: "ProfessorId", newName: "Professor_Id");
            RenameColumn(table: "dbo.Grades", name: "StudentId", newName: "Student_Id");
            AddColumn("dbo.SchoolClasses", "ProfessorId", c => c.Int(nullable: false));
            AddColumn("dbo.Grades", "StudentId", c => c.Int(nullable: false));
            CreateIndex("dbo.SchoolClasses", "Professor_Id");
            CreateIndex("dbo.Grades", "Student_Id");
        }
    }
}

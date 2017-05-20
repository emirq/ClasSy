namespace ClasSy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PivotTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfessorCourses",
                c => new
                    {
                        Professor_Id = c.String(nullable: false, maxLength: 128),
                        Course_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Professor_Id, t.Course_Id })
                .ForeignKey("dbo.Professors", t => t.Professor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Professor_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.ParentStudents",
                c => new
                    {
                        Parent_Id = c.String(nullable: false, maxLength: 128),
                        Student_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Parent_Id, t.Student_Id })
                .ForeignKey("dbo.Parents", t => t.Parent_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParentStudents", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.ParentStudents", "Parent_Id", "dbo.Parents");
            DropForeignKey("dbo.ProfessorCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.ProfessorCourses", "Professor_Id", "dbo.Professors");
            DropIndex("dbo.ParentStudents", new[] { "Student_Id" });
            DropIndex("dbo.ParentStudents", new[] { "Parent_Id" });
            DropIndex("dbo.ProfessorCourses", new[] { "Course_Id" });
            DropIndex("dbo.ProfessorCourses", new[] { "Professor_Id" });
            DropTable("dbo.ParentStudents");
            DropTable("dbo.ProfessorCourses");
        }
    }
}

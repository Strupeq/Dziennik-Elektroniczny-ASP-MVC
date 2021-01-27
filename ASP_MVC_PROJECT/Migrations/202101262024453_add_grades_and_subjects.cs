namespace ASP_MVC_PROJECT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_grades_and_subjects : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "ClassID", "dbo.Classes");
            DropIndex("dbo.Subjects", new[] { "ClassID" });
            CreateTable(
                "dbo.ClassSubjects",
                c => new
                    {
                        ClassSubjectID = c.Int(nullable: false, identity: true),
                        ClassID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassSubjectID)
                .ForeignKey("dbo.Classes", t => t.ClassID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.ClassID)
                .Index(t => t.SubjectID);
            
            DropColumn("dbo.Subjects", "ClassID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "ClassID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ClassSubjects", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.ClassSubjects", "ClassID", "dbo.Classes");
            DropIndex("dbo.ClassSubjects", new[] { "SubjectID" });
            DropIndex("dbo.ClassSubjects", new[] { "ClassID" });
            DropTable("dbo.ClassSubjects");
            CreateIndex("dbo.Subjects", "ClassID");
            AddForeignKey("dbo.Subjects", "ClassID", "dbo.Classes", "ClassID", cascadeDelete: true);
        }
    }
}

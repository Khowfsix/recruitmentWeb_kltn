using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InsternShip.Data.Entities;

public partial class RecruitmentWebContext : IdentityDbContext<IdentityUser>
{
    public RecruitmentWebContext()
    {
    }

    public RecruitmentWebContext(DbContextOptions<RecruitmentWebContext> options)
        : base(options)
    {

    }
    #region Dbset
    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<BlackList> BlackLists { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateJoinEvent> CandidateJoinEvents { get; set; }

    public virtual DbSet<CategoryQuestion> CategoryQuestions { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Cv> Cvs { get; set; }

    public virtual DbSet<CvHasSkill> CvHasSkills { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Interview> Interviews { get; set; }

    public virtual DbSet<Interviewer> Interviewers { get; set; }

    public virtual DbSet<Itrsinterview> Itrsinterviews { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionSkill> QuestionSkills { get; set; }

    public virtual DbSet<QuestionLanguage> QuestionLanguages { get; set; }

    public virtual DbSet<Recruiter> Recruiters { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Requirement> Requirements { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Round> Rounds { get; set; }

    public virtual DbSet<SecurityQuestion> SecurityQuestions { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SuccessfulCadidate> SuccessfulCadidates { get; set; }
    public virtual DbSet<ResetPassword> ResetPasswords { get; set; }

    //public virtual DbSet<WebUser> WebUsers { get; set; }
    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // => optionsBuilder.UseSqlServer("name=ConnectionStrings:DefaultConnection");
        => optionsBuilder.UseSqlServer("Data Source=localhost\\MSSQLSERVER02;Initial Catalog=RecruitmentWeb;Integrated Security=True;TrustServerCertificate=True");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
        //modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        //modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        //modelBuilder.Entity<WebUser>().ToTable("AspNetUsers");

        SeedRoles(modelBuilder);

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Applicat__C93A4C99D502D0BD");

            entity.ToTable("Application");

            entity.Property(e => e.ApplicationId).ValueGeneratedNever();
            entity.Property(e => e.Cvid).HasColumnName("Cvid");
            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Company_Status).HasMaxLength(255);
            entity.Property(e => e.Candidate_Status).HasMaxLength(255);

            //entity.HasOne(d => d.Candidate).WithMany(p => p.Applications)
            //    .HasForeignKey(d => d.CandidateId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("Fk_appliCandidate");

            entity.HasOne(d => d.Cv).WithMany(p => p.Applications)
                .HasForeignKey(d => d.Cvid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_appliCv");

            entity.HasOne(d => d.Position).WithMany(p => p.Applications)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_appliPosition");
        });

        modelBuilder.Entity<BlackList>(entity =>
        {
            entity.HasKey(e => e.BlackListId).HasName("PK__BlackLis__B54E3C741F66E917");

            entity.ToTable("BlackList");

            entity.Property(e => e.BlackListId).ValueGeneratedNever();
            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Reason).HasMaxLength(255);

            entity.HasOne(d => d.Candidate).WithMany(p => p.BlackLists)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandiInBlackList");
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.CandidateId).HasName("PK__Candidat__DF539B9C8196430E");

            entity.ToTable("Candidate");

            entity.Property(e => e.CandidateId).ValueGeneratedNever();
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

            entity.HasOne(d => d.User).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandidateUser");
        });

        modelBuilder.Entity<CandidateJoinEvent>(entity =>
        {
            entity.HasKey(e => e.CandidateJoinEventId).HasName("PK__Candidat__ECDC0AF2269C389E");

            entity.ToTable("CandidateJoinEvent");

            entity.HasIndex(e => new { e.CandidateId, e.EventId }, "UQ__Candidat__0FAC84DD20A583A2").IsUnique();

            entity.Property(e => e.CandidateJoinEventId).ValueGeneratedNever();
            entity.Property(e => e.DateJoin)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateJoinEvents)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CandiJoin");

            entity.HasOne(d => d.Event).WithMany(p => p.CandidateJoinEvents)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_joinEvent");
        });

        modelBuilder.Entity<CategoryQuestion>(entity =>
        {
            entity.HasKey(e => e.CategoryQuestionId).HasName("PK__Category__DE130A6A56DA0675");

            entity.ToTable("CategoryQuestion");
            entity.Property(e => e.CategoryQuestionId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7C122402FA9");

            entity.ToTable("Certificate");


            entity.Property(e => e.CertificateId).ValueGeneratedNever();
            entity.Property(e => e.CertificateName).HasMaxLength(255);
            entity.Property(e => e.Cvid).HasColumnName("Cvid");
            entity.Property(e => e.DateEarned).HasColumnType("date");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ExpirationDate).HasColumnType("date");
            entity.Property(e => e.OrganizationName).HasMaxLength(255);

            entity.HasOne(d => d.Cv).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.Cvid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CertificateInCV");
        });

        modelBuilder.Entity<Cv>(entity =>
        {
            entity.HasKey(e => e.Cvid).HasName("PK__CV__A04CFFA37AEDF099");

            entity.ToTable("CV");

            entity.Property(e => e.Cvid)
                .ValueGeneratedNever()
                .HasColumnName("Cvid");

            entity.Property(e => e.CvPdf).HasColumnName("CvPdf");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted")
                .HasDefaultValue(false);

            entity.HasOne(d => d.Candidate).WithMany(p => p.Cvs)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CreateCV");
        });

        modelBuilder.Entity<CvHasSkill>(entity =>
        {
            entity.HasKey(e => e.CvSkillsId).HasName("PK__CV_has_S__21EE6FE772D382E5");

            entity.ToTable("CV_has_Skills");

            entity.Property(e => e.CvSkillsId)
                .ValueGeneratedNever()
                .HasColumnName("CV_SkillsId");
            entity.Property(e => e.Cvid).HasColumnName("Cvid");
            entity.Property(e => e.ExperienceYear).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Cv).WithMany(p => p.CvHasSkills)
                .HasForeignKey(d => d.Cvid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ofCV");

            entity.HasOne(d => d.Skill).WithMany(p => p.CvHasSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_hasSkill");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED26482F76");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.DepartmentName).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Phone)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Website).HasMaxLength(255);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__7944C8101630C102");

            entity.ToTable("Event");

            entity.Property(e => e.EventId).ValueGeneratedNever();
            entity.Property(e => e.EventName).HasMaxLength(255);
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.Events)
                .HasForeignKey(d => d.RecruiterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventManagedBy");
        });

        modelBuilder.Entity<Interview>(entity =>
        {
            entity.HasKey(e => e.InterviewId).HasName("PK__Intervie__C97C58525A846D87");

            entity.ToTable("Interview");

            entity.Property(e => e.InterviewId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ItrsinterviewId).HasColumnName("ITRSInterviewId");
            entity.Property(e => e.Company_Status).HasMaxLength(255);
            entity.Property(e => e.Candidate_Status).HasMaxLength(255);

            entity.HasOne(d => d.Application).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_applicationInterview");

            entity.HasOne(d => d.Interviewer).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.InterviewerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IsConductes");

            entity.HasOne(d => d.Itrsinterview).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.ItrsinterviewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ITRS");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.RecruiterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReccerCreateInterview");

            entity.HasOne(d => d.Result).WithMany(p => p.Interviews)
                .HasForeignKey(d => d.ResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResultInterview");
        });

        modelBuilder.Entity<Interviewer>(entity =>
        {
            entity.HasKey(e => e.InterviewerId).HasName("PK__Intervie__C29BDA1D949A214A");

            entity.ToTable("Interviewer");

            entity.Property(e => e.InterviewerId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

            entity.HasOne(d => d.Department).WithMany(p => p.Interviewers)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_interDepart");

            entity.HasOne(d => d.User).WithMany(p => p.Interviewers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_InterviewerUser");
        });

        modelBuilder.Entity<Itrsinterview>(entity =>
        {
            entity.HasKey(e => e.ItrsinterviewId).HasName("PK__ITRSInte__689D871CEED2E961");

            entity.ToTable("ITRSInterview");

            entity.HasIndex(e => new { e.DateInterview, e.ShiftId, e.RoomId }, "UNIQUE_InterviewTime").IsUnique();

            entity.Property(e => e.ItrsinterviewId)
                .ValueGeneratedNever()
                .HasColumnName("ITRSInterviewId");
            entity.Property(e => e.DateInterview).HasColumnType("date");

            entity.HasOne(d => d.Room).WithMany(p => p.Itrsinterviews)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_ITRS_Room");

            entity.HasOne(d => d.Shift).WithMany(p => p.Itrsinterviews)
                .HasForeignKey(d => d.ShiftId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_ITRS_Shift");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("PK__Language__B93855AB02B6E2A3");

            entity.ToTable("Language");


            entity.Property(e => e.LanguageId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.LanguageName).HasMaxLength(255);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PK__Position__60BB9A79BADAC7AE");

            entity.ToTable("Position");

            entity.Property(e => e.PositionId).ValueGeneratedNever();

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.PositionName).HasMaxLength(255);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Department).WithMany(p => p.Positions)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hires");

            entity.HasOne(d => d.Language).WithMany(p => p.Positions)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_language");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.Positions)
                .HasForeignKey(d => d.RecruiterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ManagedBy");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FAC07D9C6DD");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).ValueGeneratedNever();

            entity.HasOne(d => d.CategoryQuestion).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CategoryQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_catQues");
        });

        modelBuilder.Entity<QuestionSkill>(entity =>
        {
            entity.HasKey(e => e.QuestionSkillsId).HasName("PK__Question__3D7C86CBF36F4D5D");

            entity.HasIndex(e => new { e.QuestionId, e.SkillId }, "UQ__Question__603A66B596184E51").IsUnique();

            entity.Property(e => e.QuestionSkillsId).ValueGeneratedNever();

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionSkills)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_SkillQues");

            entity.HasOne(d => d.Skill).WithMany(p => p.QuestionSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_QuesSkill");
        });

        modelBuilder.Entity<QuestionLanguage>(entity =>
        {
            entity.HasKey(e => e.QuestionLanguageId).HasName("PK_QuestionLanguageId");

            entity.HasIndex(e => new { e.QuestionId, e.LanguageId }, "UQ__Question_LanguageId").IsUnique();

            entity.Property(e => e.QuestionLanguageId).ValueGeneratedNever();

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionLanguages)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("Fk_LanguageQues");

            entity.HasOne(d => d.Language).WithMany(p => p.QuestionLanguages)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("Fk_QuesLanguage");
        });

        modelBuilder.Entity<Recruiter>(entity =>
        {
            entity.HasKey(e => e.RecruiterId).HasName("PK__Recruite__219CFF5625FB1B60");

            entity.ToTable("Recruiter");

            entity.Property(e => e.RecruiterId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

            entity.HasOne(d => d.Department).WithMany(p => p.Recruiters)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_reccerDepart");

            entity.HasOne(d => d.User).WithMany(p => p.Recruiters)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReccerUser");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Report__D5BD48055F400A51");

            entity.ToTable("Report");

            entity.Property(e => e.ReportId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.Reports)
                .HasForeignKey(d => d.RecruiterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReccerCreateReport");
        });

        modelBuilder.Entity<Requirement>(entity =>
        {
            entity.HasKey(e => e.RequirementId).HasName("PK__Requirem__7DF11E5D19F31719");

            entity.Property(e => e.RequirementId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

            entity.HasOne(d => d.Position).WithMany(p => p.Requirements)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_requePos");

            entity.HasOne(d => d.Skill).WithMany(p => p.Requirements)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_requeSkil");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__Result__976902081579C0D7");

            entity.ToTable("Result");

            entity.Property(e => e.ResultId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.ResultString).HasMaxLength(255);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__3286393943AEBB6D");

            entity.ToTable("Room");

            entity.HasIndex(e => e.RoomName, "UQ__Room__6B500B55E5A0FA95").IsUnique();

            entity.Property(e => e.RoomId).ValueGeneratedNever();

            entity.Property(e => e.RoomName).HasMaxLength(255);
        });

        modelBuilder.Entity<Round>(entity =>
        {
            entity.HasKey(e => e.RoundId).HasName("PK__Round__94D84DFA949E251F");

            entity.ToTable("Round");

            entity.Property(e => e.RoundId).ValueGeneratedNever();

            entity.HasOne(d => d.Interview).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.InterviewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_RoundInterview");

            entity.HasOne(d => d.Question).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_RoundQuestion");
        });


        modelBuilder.Entity<SecurityQuestion>(entity =>
        {
            entity.HasKey(e => e.SecurityQuestionId).HasName("PK__SecurityQuestion__C0A83881EF08EB13");

            entity.ToTable("SecurityQuestion");

            entity.Property(e => e.SecurityQuestionId);
            entity.Property(e => e.QuestionString).HasMaxLength(255);
        });

        modelBuilder.Entity<SecurityAnswer>(entity =>
        {
            entity.HasKey(e => e.SecurityAnswerId).HasName("PK__SecurityAnswer__C0A83881EF08EB13");

            entity.ToTable("SecurityAnswer");

            entity.Property(e => e.SecurityAnswerId);

            entity.HasOne(e => e.SecurityQuestion).WithMany(r => r.SecurityAnswers)
                .HasForeignKey(e => e.SecurityQuestionId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_AnswerForQues");

            //entity.HasOne(e => e.WebUser).WithMany(r => r.SecurityAnswers)
            //    .HasForeignKey(e => e.WebUserId)
            //    .OnDelete(DeleteBehavior.ClientCascade)
            //    .HasConstraintName("FK_AnswerForUser");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("PK__Shift__C0A83881EF08EB13");

            entity.ToTable("Shift");

            entity.Property(e => e.ShiftId).ValueGeneratedNever();

        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__Skill__DFA0918741CB17C8");

            entity.ToTable("Skill");

            entity.Property(e => e.SkillId).ValueGeneratedNever();

            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted")
                .HasDefaultValue(false);
            entity.Property(e => e.SkillName).HasMaxLength(255);
        });

        modelBuilder.Entity<SuccessfulCadidate>(entity =>
        {
            entity.HasKey(e => e.SuccessfulCadidateId).HasName("PK__Successf__0743315651E595B0");

            entity.ToTable("SuccessfulCadidate");


            entity.Property(e => e.SuccessfulCadidateId).ValueGeneratedNever();

            entity.Property(e => e.DateSuccess)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

            entity.HasOne(d => d.Candidate).WithMany(p => p.SuccessfulCadidates)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuccessfulCandi");

            entity.HasOne(d => d.Position).WithMany(p => p.SuccessfulCadidates)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuccessfulPosition");
        });

        base.OnModelCreating(modelBuilder);
    }

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    private void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData
            (
            new IdentityRole() { Name = "Candidate", ConcurrencyStamp = "1", NormalizedName = "Candidate" },
            new IdentityRole() { Name = "Interviewer", ConcurrencyStamp = "2", NormalizedName = "Interviewer" },
            new IdentityRole() { Name = "Recruiter", ConcurrencyStamp = "3", NormalizedName = "Recruiter" },
            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "4", NormalizedName = "Admin" }
            );

        //FormattableString triggerCommand = String(;
        //Database.ExecuteSql(triggerCommand);
    }
}

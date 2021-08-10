using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DBContext.Models;

namespace DBContext.Connect
{
    public partial class MediaStudioContext : DbContext
    {
        public MediaStudioContext()
        {
        }

        public MediaStudioContext(DbContextOptions<MediaStudioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Album> Album { get; set; }
        public virtual DbSet<AlbumStorage> AlbumStorage { get; set; }
        public virtual DbSet<AlbumToProperties> AlbumToProperties { get; set; }
        public virtual DbSet<AuditAccount> AuditAccount { get; set; }
        public virtual DbSet<AuditAlbum> AuditAlbum { get; set; }
        public virtual DbSet<AuditPerformer> AuditPerformer { get; set; }
        public virtual DbSet<AuditProperties> AuditProperties { get; set; }
        public virtual DbSet<AuditTrack> AuditTrack { get; set; }
        public virtual DbSet<AuthHistory> AuthHistory { get; set; }
        public virtual DbSet<AuthStatus> AuthStatus { get; set; }
        public virtual DbSet<Colour> Colour { get; set; }
        public virtual DbSet<GroupPropToTypeAudio> GroupPropToTypeAudio { get; set; }
        public virtual DbSet<GroupProperties> GroupProperties { get; set; }
        public virtual DbSet<Page> Page { get; set; }
        public virtual DbSet<PageSection> PageSection { get; set; }
        public virtual DbSet<Performer> Performer { get; set; }
        public virtual DbSet<PerformerToTrack> PerformerToTrack { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<PlaylistStorage> PlaylistStorage { get; set; }
        public virtual DbSet<PlaylistToProperties> PlaylistToProperties { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<SectionAlbum> SectionAlbum { get; set; }
        public virtual DbSet<SectionPlaylist> SectionPlaylist { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<StorageBucket> StorageBucket { get; set; }
        public virtual DbSet<Track> Track { get; set; }
        public virtual DbSet<TrackStorage> TrackStorage { get; set; }
        public virtual DbSet<TrackToAlbum> TrackToAlbum { get; set; }
        public virtual DbSet<TrackToPlaylist> TrackToPlaylist { get; set; }
        public virtual DbSet<TrackToProperties> TrackToProperties { get; set; }
        public virtual DbSet<TypeAccount> TypeAccount { get; set; }
        public virtual DbSet<TypeAudio> TypeAudio { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserHistoryAlbum> UserHistoryAlbum { get; set; }
        public virtual DbSet<UserHistoryPlaylist> UserHistoryPlaylist { get; set; }
        public virtual DbSet<UserHistoryTrack> UserHistoryTrack { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=185.248.102.115; Port=5432; Database=mediastudio; User Id=postgres; Password=ft5tf&XuwRe4Db;CommandTimeout=300;Pooling=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAccount)
                    .HasName("account_pkey");

                entity.ToTable("Account", "service");

                entity.HasIndex(e => e.Login)
                    .HasName("account_login_key")
                    .IsUnique();

                entity.Property(e => e.IdAccount).HasColumnName("id_account");

                entity.Property(e => e.IdTypeAccount).HasColumnName("id_type_account");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.TimeRegistration)
                    .HasColumnName("time_registration")
                    .HasColumnType("timestamp(0) with time zone")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.IdTypeAccountNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.IdTypeAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Account_fk");
            });

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.IdAlbum)
                    .HasName("Album_pkey");

                entity.ToTable("Album", "audio");

                entity.Property(e => e.IdAlbum).HasColumnName("id_album");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.HighQualityExist)
                    .HasColumnName("high_quality_exist")
                    .HasComment("у альбома есть треки в HQ качестве");

                entity.Property(e => e.IdTypeAudio).HasColumnName("id_type_audio");

                entity.Property(e => e.IsChecked)
                    .HasColumnName("is_checked")
                    .HasComment("является ли альбом одобренный модертором");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("is_delete")
                    .HasComment("удален ли альбом");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(70);

                entity.Property(e => e.PublicationTime)
                    .HasColumnName("publication_time")
                    .HasColumnType("timestamp(0) with time zone")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ReleaseYear)
                    .HasColumnName("release_year")
                    .HasComment("год выпуска");

                entity.HasOne(d => d.IdTypeAudioNavigation)
                    .WithMany(p => p.Album)
                    .HasForeignKey(d => d.IdTypeAudio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Album_fk");
            });

            modelBuilder.Entity<AlbumStorage>(entity =>
            {
                entity.HasKey(e => e.IdAlbumStorage)
                    .HasName("AlbumCloud_pkey");

                entity.ToTable("AlbumStorage", "resources");

                entity.HasIndex(e => e.IdAbum)
                    .HasName("AlbumCloud_idx_id_album");

                entity.Property(e => e.IdAlbumStorage)
                    .HasColumnName("id_album_storage")
                    .HasDefaultValueSql("nextval('resources.\"AlbumCloud_id_album_cloud_seq\"'::regclass)");

                entity.Property(e => e.IdAbum).HasColumnName("id_abum");

                entity.Property(e => e.IdStorage).HasColumnName("id_storage");

                entity.HasOne(d => d.IdAbumNavigation)
                    .WithMany(p => p.AlbumStorage)
                    .HasForeignKey(d => d.IdAbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AlbumCloud_fk");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.AlbumStorage)
                    .HasForeignKey(d => d.IdStorage)
                    .HasConstraintName("AlbumCloud_fk1");
            });

            modelBuilder.Entity<AlbumToProperties>(entity =>
            {
                entity.HasKey(e => e.IdAlbumToProperties)
                    .HasName("AlbumProperties_pkey");

                entity.ToTable("AlbumToProperties", "audio");

                entity.Property(e => e.IdAlbumToProperties)
                    .HasColumnName("id_album_to_properties")
                    .HasDefaultValueSql("nextval('audio.\"AlbumProperties_id_album_properties_seq\"'::regclass)");

                entity.Property(e => e.IdAlbum).HasColumnName("id_album");

                entity.Property(e => e.IdProp).HasColumnName("id_prop");

                entity.HasOne(d => d.IdAlbumNavigation)
                    .WithMany(p => p.AlbumToProperties)
                    .HasForeignKey(d => d.IdAlbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AlbumProperties_fk");

                entity.HasOne(d => d.IdPropNavigation)
                    .WithMany(p => p.AlbumToProperties)
                    .HasForeignKey(d => d.IdProp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AlbumProperties_fk1");
            });

            modelBuilder.Entity<AuditAccount>(entity =>
            {
                entity.HasKey(e => e.IdAuditAccount)
                    .HasName("audit_account_pkey");

                entity.ToTable("audit_account", "user_audit");

                entity.Property(e => e.IdAuditAccount).HasColumnName("id_audit_account");

                entity.Property(e => e.AccountToEdit)
                    .HasColumnName("account_to_edit")
                    .HasMaxLength(50)
                    .HasComment("какой аккаунт редактировали");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.ExecutorLogin)
                    .IsRequired()
                    .HasColumnName("executor_login")
                    .HasMaxLength(50)
                    .HasComment("кто редактировал");

                entity.Property(e => e.IdAccount)
                    .HasColumnName("id_account")
                    .HasComment("что редактировали");

                entity.Property(e => e.IsSuccessful).HasColumnName("is_successful");

                entity.Property(e => e.OldValue).HasColumnName("old_value");

                entity.Property(e => e.TimeOperation)
                    .HasColumnName("time_operation")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<AuditAlbum>(entity =>
            {
                entity.HasKey(e => e.IdAuditAlbum)
                    .HasName("album_history_pkey");

                entity.ToTable("audit_album", "user_audit");

                entity.Property(e => e.IdAuditAlbum).HasColumnName("id_audit_album");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.ExecutorLogin)
                    .IsRequired()
                    .HasColumnName("executor_login")
                    .HasMaxLength(50);

                entity.Property(e => e.IdAlbum).HasColumnName("id_album");

                entity.Property(e => e.IsSuccessful).HasColumnName("is_successful");

                entity.Property(e => e.NameAlbum)
                    .HasColumnName("name_album")
                    .HasMaxLength(60);

                entity.Property(e => e.OldValue).HasColumnName("old_value");

                entity.Property(e => e.TimeOperation)
                    .HasColumnName("time_operation")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<AuditPerformer>(entity =>
            {
                entity.HasKey(e => e.IdAuditPerformer)
                    .HasName("audit_performer_album_history_pkey");

                entity.ToTable("audit_performer", "user_audit");

                entity.Property(e => e.IdAuditPerformer)
                    .HasColumnName("id_audit_performer")
                    .HasDefaultValueSql("nextval(('user_audit.audit_performer_id_audit_performer_seq'::text)::regclass)");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.ExecutorLogin)
                    .IsRequired()
                    .HasColumnName("executor_login")
                    .HasMaxLength(50);

                entity.Property(e => e.IdPerformer).HasColumnName("id_performer");

                entity.Property(e => e.IsSuccessful).HasColumnName("is_successful");

                entity.Property(e => e.NamePerformer)
                    .HasColumnName("name_performer")
                    .HasMaxLength(60);

                entity.Property(e => e.OldValue).HasColumnName("old_value");

                entity.Property(e => e.TimeOperation)
                    .HasColumnName("time_operation")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<AuditProperties>(entity =>
            {
                entity.HasKey(e => e.IdAuditProp)
                    .HasName("audit_properties_album_history_pkey");

                entity.ToTable("audit_properties", "user_audit");

                entity.Property(e => e.IdAuditProp)
                    .HasColumnName("id_audit_prop")
                    .HasDefaultValueSql("nextval('user_audit.audit_properties_id_audit_album_seq'::regclass)");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.ExecutorLogin)
                    .IsRequired()
                    .HasColumnName("executor_login")
                    .HasMaxLength(50);

                entity.Property(e => e.IdProp).HasColumnName("id_prop");

                entity.Property(e => e.IsSuccessful).HasColumnName("is_successful");

                entity.Property(e => e.NameProp)
                    .HasColumnName("name_prop")
                    .HasMaxLength(50);

                entity.Property(e => e.OldValue).HasColumnName("old_value");

                entity.Property(e => e.TimeOperation)
                    .HasColumnName("time_operation")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<AuditTrack>(entity =>
            {
                entity.HasKey(e => e.IdAuditTrack)
                    .HasName("audit_track_album_history_pkey");

                entity.ToTable("audit_track", "user_audit");

                entity.Property(e => e.IdAuditTrack).HasColumnName("id_audit_track");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.ExecutorLogin)
                    .IsRequired()
                    .HasColumnName("executor_login")
                    .HasMaxLength(50);

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.Property(e => e.IsSuccessful).HasColumnName("is_successful");

                entity.Property(e => e.NameTrack)
                    .HasColumnName("name_track")
                    .HasMaxLength(100);

                entity.Property(e => e.OldValue).HasColumnName("old_value");

                entity.Property(e => e.TimeOperation)
                    .HasColumnName("time_operation")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<AuthHistory>(entity =>
            {
                entity.HasKey(e => e.IdAuthHistory)
                    .HasName("audit_auth_pkey");

                entity.ToTable("auth_history", "user_audit");

                entity.Property(e => e.IdAuthHistory).HasColumnName("id_auth_history");

                entity.Property(e => e.Action).HasColumnName("action");

                entity.Property(e => e.ExecutorLogin)
                    .HasColumnName("executor_login")
                    .HasMaxLength(50)
                    .HasComment("кто редактировал");

                entity.Property(e => e.Ipv4)
                    .HasColumnName("IPv4")
                    .HasMaxLength(16);

                entity.Property(e => e.IsSuccessful).HasColumnName("is_successful");

                entity.Property(e => e.NameDevice)
                    .HasColumnName("name_device")
                    .HasMaxLength(70);

                entity.Property(e => e.TimeAction)
                    .HasColumnName("time_action")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.UserAgent)
                    .HasColumnName("user_agent")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<AuthStatus>(entity =>
            {
                entity.HasKey(e => e.IdAuthStatus)
                    .HasName("auth_status_pkey");

                entity.ToTable("auth_status", "system");

                entity.HasIndex(e => e.Jwt)
                    .HasName("auth_status_jwt_key")
                    .IsUnique();

                entity.HasIndex(e => e.Refresh)
                    .HasName("auth_status_refresh_key")
                    .IsUnique();

                entity.Property(e => e.IdAuthStatus).HasColumnName("id_auth_status");

                entity.Property(e => e.Ipv4)
                    .HasColumnName("IPv4")
                    .HasMaxLength(16);

                entity.Property(e => e.IsValid)
                    .HasColumnName("is_valid")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.Jwt)
                    .IsRequired()
                    .HasColumnName("jwt")
                    .HasComment("access токен");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50);

                entity.Property(e => e.NameDevice)
                    .HasColumnName("name_device")
                    .HasMaxLength(70);

                entity.Property(e => e.Refresh)
                    .IsRequired()
                    .HasColumnName("refresh")
                    .HasMaxLength(150)
                    .HasComment("refresh токен");

                entity.Property(e => e.UserAgent)
                    .HasColumnName("user_agent")
                    .HasMaxLength(150)
                    .HasComment("пользовательское приложение");

                entity.Property(e => e.ValidFrom)
                    .HasColumnName("valid from")
                    .HasColumnType("timestamp(0) with time zone")
                    .HasComment("действует с");

                entity.Property(e => e.ValidUntil)
                    .HasColumnName("valid_until")
                    .HasColumnType("timestamp(0) with time zone")
                    .HasComment("действует до");
            });

            modelBuilder.Entity<Colour>(entity =>
            {
                entity.HasKey(e => e.IdColour)
                    .HasName("Colour_pkey");

                entity.ToTable("Colour", "ref");

                entity.HasIndex(e => e.Code)
                    .HasName("Colour_code_key")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("Colour_colour_key")
                    .IsUnique();

                entity.Property(e => e.IdColour).HasColumnName("id_colour");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(8);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<GroupPropToTypeAudio>(entity =>
            {
                entity.HasKey(e => e.IdPk)
                    .HasName("TypeAudioToGroupProp_pkey");

                entity.ToTable("GroupPropToTypeAudio", "audio");

                entity.Property(e => e.IdPk)
                    .HasColumnName("id_pk")
                    .HasDefaultValueSql("nextval('audio.\"TypeAudioToGroupProp_id_pk_seq\"'::regclass)");

                entity.Property(e => e.IdGroupProp).HasColumnName("id_group_prop");

                entity.Property(e => e.IdTypeAudio).HasColumnName("id_type_audio");

                entity.Property(e => e.IsNecessary)
                    .HasColumnName("is_necessary")
                    .HasComment("обязательно ли  свойство группы для типа аудио");

                entity.HasOne(d => d.IdGroupPropNavigation)
                    .WithMany(p => p.GroupPropToTypeAudio)
                    .HasForeignKey(d => d.IdGroupProp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TypeAudioToGroupProp_fk");

                entity.HasOne(d => d.IdTypeAudioNavigation)
                    .WithMany(p => p.GroupPropToTypeAudio)
                    .HasForeignKey(d => d.IdTypeAudio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TypeAudioToGroupProp_fk1");
            });

            modelBuilder.Entity<GroupProperties>(entity =>
            {
                entity.HasKey(e => e.IdGroupProp)
                    .HasName("TypeProperties_pkey");

                entity.ToTable("GroupProperties", "audio");

                entity.Property(e => e.IdGroupProp)
                    .HasColumnName("id_group_prop")
                    .HasDefaultValueSql("nextval('audio.\"TypeProperties_id_type_properties_seq\"'::regclass)");

                entity.Property(e => e.AllowMultiselect).HasColumnName("allow_multiselect");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.HasKey(e => e.IdPage)
                    .HasName("page_pkey");

                entity.ToTable("page", "interface");

                entity.HasIndex(e => e.Name)
                    .HasName("page_name_key")
                    .IsUnique();

                entity.Property(e => e.IdPage).HasColumnName("id_page");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<PageSection>(entity =>
            {
                entity.HasKey(e => e.IdSectionPage)
                    .HasName("section_to_page_pkey");

                entity.ToTable("page_section", "interface");

                entity.Property(e => e.IdSectionPage)
                    .HasColumnName("id_section_page")
                    .HasDefaultValueSql("nextval('interface.section_to_page_id_section_page_seq'::regclass)");

                entity.Property(e => e.IdPage).HasColumnName("id_page");

                entity.Property(e => e.IdSection).HasColumnName("id_section");

                entity.Property(e => e.OrderSection).HasColumnName("order_section");

                entity.HasOne(d => d.IdPageNavigation)
                    .WithMany(p => p.PageSection)
                    .HasForeignKey(d => d.IdPage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("section_to_page_fk1");

                entity.HasOne(d => d.IdSectionNavigation)
                    .WithMany(p => p.PageSection)
                    .HasForeignKey(d => d.IdSection)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("section_to_page_fk");
            });

            modelBuilder.Entity<Performer>(entity =>
            {
                entity.HasKey(e => e.IdPerformer)
                    .HasName("Performer_pkey");

                entity.ToTable("Performer", "audio");

                entity.Property(e => e.IdPerformer).HasColumnName("id_performer");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<PerformerToTrack>(entity =>
            {
                entity.HasKey(e => e.IdPerformerToTrack)
                    .HasName("PerformerToTrack_pkey");

                entity.ToTable("PerformerToTrack", "audio");

                entity.Property(e => e.IdPerformerToTrack).HasColumnName("id_performer_to_track");

                entity.Property(e => e.IdPerformer).HasColumnName("id_performer");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.HasOne(d => d.IdPerformerNavigation)
                    .WithMany(p => p.PerformerToTrack)
                    .HasForeignKey(d => d.IdPerformer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PerformerToTrack_fk");

                entity.HasOne(d => d.IdTrackNavigation)
                    .WithMany(p => p.PerformerToTrack)
                    .HasForeignKey(d => d.IdTrack)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PerformerToTrack_fk1");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.HasKey(e => e.IdPlaylist)
                    .HasName("Playlist_pkey");

                entity.ToTable("Playlist", "audio");

                entity.Property(e => e.IdPlaylist).HasColumnName("id_playlist");

                entity.Property(e => e.IdAccount).HasColumnName("id_account");

                entity.Property(e => e.IdColour).HasColumnName("id_colour");

                entity.Property(e => e.IsPublic)
                    .HasColumnName("is_public")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(40);

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithMany(p => p.Playlist)
                    .HasForeignKey(d => d.IdAccount)
                    .HasConstraintName("Playlist_fk");

                entity.HasOne(d => d.IdColourNavigation)
                    .WithMany(p => p.Playlist)
                    .HasForeignKey(d => d.IdColour)
                    .HasConstraintName("Playlist_fk1");
            });

            modelBuilder.Entity<PlaylistStorage>(entity =>
            {
                entity.HasKey(e => e.IdPlaylistStorage)
                    .HasName("PlaylistCloud_pkey");

                entity.ToTable("PlaylistStorage", "resources");

                entity.Property(e => e.IdPlaylistStorage)
                    .HasColumnName("id_playlist_storage")
                    .HasDefaultValueSql("nextval('resources.\"PlaylistCloud_id_playlist_cloud_seq\"'::regclass)");

                entity.Property(e => e.IdPlaylist).HasColumnName("id_playlist");

                entity.Property(e => e.IdStorage).HasColumnName("id_storage");

                entity.HasOne(d => d.IdPlaylistNavigation)
                    .WithMany(p => p.PlaylistStorage)
                    .HasForeignKey(d => d.IdPlaylist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlaylistCloud_fk1");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.PlaylistStorage)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlaylistCloud_fk");
            });

            modelBuilder.Entity<PlaylistToProperties>(entity =>
            {
                entity.HasKey(e => e.IdPaylistToProperties)
                    .HasName("PlaylistToProperties_pkey");

                entity.ToTable("PlaylistToProperties", "audio");

                entity.Property(e => e.IdPaylistToProperties).HasColumnName("id_paylist_to_properties");

                entity.Property(e => e.IdPlaylist).HasColumnName("id_playlist");

                entity.Property(e => e.IdProp).HasColumnName("id_prop");

                entity.HasOne(d => d.IdPlaylistNavigation)
                    .WithMany(p => p.PlaylistToProperties)
                    .HasForeignKey(d => d.IdPlaylist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlaylistToProperties_fk");

                entity.HasOne(d => d.IdPropNavigation)
                    .WithMany(p => p.PlaylistToProperties)
                    .HasForeignKey(d => d.IdProp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PlaylistToProperties_fk1");
            });

            modelBuilder.Entity<Properties>(entity =>
            {
                entity.HasKey(e => e.IdProp)
                    .HasName("Properties_pkey");

                entity.ToTable("Properties", "audio");

                entity.Property(e => e.IdProp).HasColumnName("id_prop");

                entity.Property(e => e.IdColour).HasColumnName("id_colour");

                entity.Property(e => e.IdGroupProp).HasColumnName("id_group_prop");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdColourNavigation)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.IdColour)
                    .HasConstraintName("Properties_fk1");

                entity.HasOne(d => d.IdGroupPropNavigation)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.IdGroupProp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Properties_fk");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.HasKey(e => e.IdSection)
                    .HasName("section_pkey");

                entity.ToTable("section", "interface");

                entity.Property(e => e.IdSection)
                    .HasColumnName("id_section")
                    .HasDefaultValueSql("nextval('interface.section_section_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(70);
            });

            modelBuilder.Entity<SectionAlbum>(entity =>
            {
                entity.HasKey(e => e.IdSectionAlbum)
                    .HasName("section_album_pkey");

                entity.ToTable("section_album", "interface");

                entity.Property(e => e.IdSectionAlbum).HasColumnName("id_section_album");

                entity.Property(e => e.IdAlbum).HasColumnName("id_album");

                entity.Property(e => e.IdSection).HasColumnName("id_section");

                entity.HasOne(d => d.IdAlbumNavigation)
                    .WithMany(p => p.SectionAlbum)
                    .HasForeignKey(d => d.IdAlbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("section_album_fk1");

                entity.HasOne(d => d.IdSectionNavigation)
                    .WithMany(p => p.SectionAlbum)
                    .HasForeignKey(d => d.IdSection)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("section_album_fk");
            });

            modelBuilder.Entity<SectionPlaylist>(entity =>
            {
                entity.HasKey(e => e.IdSectionPlaylist)
                    .HasName("section_item_pkey");

                entity.ToTable("section_playlist", "interface");

                entity.Property(e => e.IdSectionPlaylist)
                    .HasColumnName("id_section_playlist")
                    .HasDefaultValueSql("nextval('interface.section_item_section_item_seq'::regclass)");

                entity.Property(e => e.IdPlaylist).HasColumnName("id_playlist");

                entity.Property(e => e.IdSection).HasColumnName("id_section");

                entity.HasOne(d => d.IdPlaylistNavigation)
                    .WithMany(p => p.SectionPlaylist)
                    .HasForeignKey(d => d.IdPlaylist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("section_playlist_fk1");

                entity.HasOne(d => d.IdSectionNavigation)
                    .WithMany(p => p.SectionPlaylist)
                    .HasForeignKey(d => d.IdSection)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("section_playlist_fk");
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(e => e.IdStorage)
                    .HasName("Cloud_pkey");

                entity.ToTable("Storage", "resources");

                entity.HasIndex(e => e.IdBucket)
                    .HasName("Cloud_idx_id_bucket");

                entity.HasIndex(e => e.ObjectName)
                    .HasName("Cloud_name_object_key")
                    .IsUnique();

                entity.HasIndex(e => e.StaticUrl)
                    .HasName("Cloud_public_url_key")
                    .IsUnique();

                entity.HasIndex(e => e.TemporaryUrl)
                    .HasName("Cloud_temporary_url_key")
                    .IsUnique();

                entity.Property(e => e.IdStorage)
                    .HasColumnName("id_storage")
                    .HasDefaultValueSql("nextval('resources.\"Cloud_id_cloud_seq\"'::regclass)");

                entity.Property(e => e.IdBucket).HasColumnName("id_bucket");

                entity.Property(e => e.ObjectName)
                    .IsRequired()
                    .HasColumnName("object_name")
                    .HasMaxLength(60)
                    .HasComment("имя файла в папке");

                entity.Property(e => e.StaticUrl)
                    .HasColumnName("static_url")
                    .HasMaxLength(120)
                    .HasComment("постоянная ссылка");

                entity.Property(e => e.TemporaryUrl)
                    .HasColumnName("temporary_url")
                    .HasComment("временная ссылка");

                entity.Property(e => e.ValidUntil)
                    .HasColumnName("valid_until")
                    .HasColumnType("timestamp(0) with time zone")
                    .HasComment("действует до");

                entity.HasOne(d => d.IdBucketNavigation)
                    .WithMany(p => p.Storage)
                    .HasForeignKey(d => d.IdBucket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Cloud_fk");
            });

            modelBuilder.Entity<StorageBucket>(entity =>
            {
                entity.HasKey(e => e.IdStorageBucket)
                    .HasName("cloud_bucket_pkey");

                entity.ToTable("StorageBucket", "resources");

                entity.HasIndex(e => e.NameBucket)
                    .HasName("cloud_bucket_name_bucket_key")
                    .IsUnique();

                entity.Property(e => e.IdStorageBucket)
                    .HasColumnName("id_storage_bucket")
                    .HasDefaultValueSql("nextval('resources.cloud_bucket_id_cloud_bucket_seq'::regclass)");

                entity.Property(e => e.NameBucket)
                    .IsRequired()
                    .HasColumnName("name_bucket")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Track>(entity =>
            {
                entity.HasKey(e => e.IdTrack)
                    .HasName("Track_pkey");

                entity.ToTable("Track", "audio");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.Property(e => e.AlbumOrder)
                    .HasColumnName("album_order")
                    .HasComment("порядок трека в альбоме");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasComment("длительность аудио трека (сек)");

                entity.Property(e => e.IdTypeAudio).HasColumnName("id_type_audio");

                entity.Property(e => e.IsDelete)
                    .HasColumnName("is_delete")
                    .HasComment("удален ли трек");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.PublicationTime)
                    .HasColumnName("publication_time")
                    .HasColumnType("timestamp(0) with time zone")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.IdTypeAudioNavigation)
                    .WithMany(p => p.Track)
                    .HasForeignKey(d => d.IdTypeAudio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Track_fk");
            });

            modelBuilder.Entity<TrackStorage>(entity =>
            {
                entity.HasKey(e => e.IdTrackStorage)
                    .HasName("TrackCloud_pkey");

                entity.ToTable("TrackStorage", "resources");

                entity.HasIndex(e => e.IdTrack)
                    .HasName("TrackCloud_idx_id_track");

                entity.Property(e => e.IdTrackStorage)
                    .HasColumnName("id_track_storage")
                    .HasDefaultValueSql("nextval('resources.\"TrackCloud_id_track_cloud_seq\"'::regclass)");

                entity.Property(e => e.IdStorage).HasColumnName("id_storage");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.HasOne(d => d.IdStorageNavigation)
                    .WithMany(p => p.TrackStorage)
                    .HasForeignKey(d => d.IdStorage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TrackCloud_fk");

                entity.HasOne(d => d.IdTrackNavigation)
                    .WithMany(p => p.TrackStorage)
                    .HasForeignKey(d => d.IdTrack)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TrackCloud_fk1");
            });

            modelBuilder.Entity<TrackToAlbum>(entity =>
            {
                entity.HasKey(e => e.IdTrackToAlbum)
                    .HasName("TrackToAlbum_pkey");

                entity.ToTable("TrackToAlbum", "audio");

                entity.Property(e => e.IdTrackToAlbum).HasColumnName("id_track_to_album");

                entity.Property(e => e.IdAlbum).HasColumnName("id_album");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.HasOne(d => d.IdAlbumNavigation)
                    .WithMany(p => p.TrackToAlbum)
                    .HasForeignKey(d => d.IdAlbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TrackToAlbum_fk1");

                entity.HasOne(d => d.IdTrackNavigation)
                    .WithMany(p => p.TrackToAlbum)
                    .HasForeignKey(d => d.IdTrack)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TrackToAlbum_fk");
            });

            modelBuilder.Entity<TrackToPlaylist>(entity =>
            {
                entity.HasKey(e => e.IdTrackToPlaylist)
                    .HasName("TrackToPlaylist_pkey");

                entity.ToTable("TrackToPlaylist", "audio");

                entity.Property(e => e.IdTrackToPlaylist).HasColumnName("id_track_to_playlist");

                entity.Property(e => e.IdPlaylist).HasColumnName("id_playlist");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.Property(e => e.Order).HasColumnName("order");

                entity.HasOne(d => d.IdPlaylistNavigation)
                    .WithMany(p => p.TrackToPlaylist)
                    .HasForeignKey(d => d.IdPlaylist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TrackToPlaylist_fk");

                entity.HasOne(d => d.IdTrackNavigation)
                    .WithMany(p => p.TrackToPlaylist)
                    .HasForeignKey(d => d.IdTrack)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TrackToPlaylist_fk1");
            });

            modelBuilder.Entity<TrackToProperties>(entity =>
            {
                entity.HasKey(e => e.IdTrackToProperties)
                    .HasName("TrackProperties_pkey");

                entity.ToTable("TrackToProperties", "audio");

                entity.Property(e => e.IdTrackToProperties)
                    .HasColumnName("id_track_to_properties")
                    .HasDefaultValueSql("nextval('audio.\"TrackProperties_id_track_properties_seq\"'::regclass)");

                entity.Property(e => e.IdProp).HasColumnName("id_prop");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.HasOne(d => d.IdPropNavigation)
                    .WithMany(p => p.TrackToProperties)
                    .HasForeignKey(d => d.IdProp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TrackProperties_fk1");

                entity.HasOne(d => d.IdTrackNavigation)
                    .WithMany(p => p.TrackToProperties)
                    .HasForeignKey(d => d.IdTrack)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TrackProperties_fk");
            });

            modelBuilder.Entity<TypeAccount>(entity =>
            {
                entity.HasKey(e => e.IdTypeAccount)
                    .HasName("type_account_pkey");

                entity.ToTable("TypeAccount", "service");

                entity.HasIndex(e => e.NameType)
                    .HasName("type_account_name_ttype_key")
                    .IsUnique();

                entity.Property(e => e.IdTypeAccount)
                    .HasColumnName("id_type_account")
                    .HasDefaultValueSql("nextval('service.type_account_id_type_account_seq'::regclass)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(60);

                entity.Property(e => e.NameType)
                    .IsRequired()
                    .HasColumnName("name_type")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<TypeAudio>(entity =>
            {
                entity.HasKey(e => e.IdTypeAudio)
                    .HasName("TypeAudio_pkey");

                entity.ToTable("TypeAudio", "audio");

                entity.Property(e => e.IdTypeAudio).HasColumnName("id_type_audio");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("User_pkey");

                entity.ToTable("User", "service");

                entity.HasIndex(e => e.IdAccount)
                    .HasName("User_id_account_key")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.DateBirthday)
                    .HasColumnName("date_birthday")
                    .HasColumnType("date")
                    .HasComment("дата рождения");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(18);

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasComment("пол");

                entity.Property(e => e.IdAccount).HasColumnName("id_account");

                entity.Property(e => e.IdCloudPath)
                    .HasColumnName("id_cloud_path")
                    .HasComment("ссылка на id картинки профиля");

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(18)
                    .HasComment("фамилия");

                entity.Property(e => e.Patronymic)
                    .HasColumnName("patronymic")
                    .HasMaxLength(18)
                    .HasComment("отчество");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasMaxLength(20)
                    .HasComment("номер телефона");

                entity.HasOne(d => d.IdAccountNavigation)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.IdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User_fk");
            });

            modelBuilder.Entity<UserHistoryAlbum>(entity =>
            {
                entity.HasKey(e => e.IdUserHistoryTrack)
                    .HasName("user_history_album_pkey");

                entity.ToTable("user_history_album", "user_history");

                entity.Property(e => e.IdUserHistoryTrack).HasColumnName("id_user_history_track");

                entity.Property(e => e.IdAlbum).HasColumnName("id_album");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.LastUse)
                    .HasColumnName("last_use")
                    .HasColumnType("timestamp(0) without time zone")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.IdAlbumNavigation)
                    .WithMany(p => p.UserHistoryAlbum)
                    .HasForeignKey(d => d.IdAlbum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_history_album_fk");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserHistoryAlbum)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_history_album_fk1");
            });

            modelBuilder.Entity<UserHistoryPlaylist>(entity =>
            {
                entity.HasKey(e => e.IdUserHistoryTrack)
                    .HasName("user_history_playlist_pkey");

                entity.ToTable("user_history_playlist", "user_history");

                entity.Property(e => e.IdUserHistoryTrack).HasColumnName("id_user_history_track");

                entity.Property(e => e.IdPlaylist).HasColumnName("id_playlist");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.LastUse)
                    .HasColumnName("last_use")
                    .HasColumnType("timestamp(0) without time zone")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.IdPlaylistNavigation)
                    .WithMany(p => p.UserHistoryPlaylist)
                    .HasForeignKey(d => d.IdPlaylist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_history_playlist_fk");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserHistoryPlaylist)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_history_playlist_fk1");
            });

            modelBuilder.Entity<UserHistoryTrack>(entity =>
            {
                entity.HasKey(e => e.IdUserHistoryTrack)
                    .HasName("user_history_track_pkey");

                entity.ToTable("user_history_track", "user_history");

                entity.Property(e => e.IdUserHistoryTrack).HasColumnName("id_user_history_track");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.LastUse)
                    .HasColumnName("last_use")
                    .HasColumnType("timestamp(0) without time zone")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.IdTrackNavigation)
                    .WithMany(p => p.UserHistoryTrack)
                    .HasForeignKey(d => d.IdTrack)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_history_track_fk");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserHistoryTrack)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_history_track_fk1");
            });

            modelBuilder.HasSequence("audit_performer_id_audit_performer_seq", "user_audit");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

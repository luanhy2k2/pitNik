IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240615083956_intial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240615083956_intial', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [Conversations] (
        [Id] int NOT NULL IDENTITY,
        [User1Id] nvarchar(450) NOT NULL,
        [User2Id] nvarchar(450) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Conversations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Conversations_AspNetUsers_User1Id] FOREIGN KEY ([User1Id]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Conversations_AspNetUsers_User2Id] FOREIGN KEY ([User2Id]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [Emojis] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Emojis] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [Friendships] (
        [Id] int NOT NULL IDENTITY,
        [SenderId] nvarchar(450) NOT NULL,
        [ReceiverId] nvarchar(450) NOT NULL,
        [Status] int NOT NULL,
        [RequestedAt] datetime2 NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Friendships] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Friendships_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Friendships_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [Groups] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Groups] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [Posts] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Posts_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [Messages] (
        [Id] int NOT NULL IDENTITY,
        [SenderId] nvarchar(450) NOT NULL,
        [ReceiverId] nvarchar(450) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [ConversationId] int NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Messages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Messages_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_Messages_Conversations_ConversationId] FOREIGN KEY ([ConversationId]) REFERENCES [Conversations] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [GroupMembers] (
        [Id] int NOT NULL IDENTITY,
        [GroupId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [JoinedAt] datetime2 NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_GroupMembers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GroupMembers_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GroupMembers_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [GroupMessage] (
        [Id] int NOT NULL IDENTITY,
        [GroupId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_GroupMessage] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GroupMessage_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_GroupMessage_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [Comments] (
        [Id] int NOT NULL IDENTITY,
        [PostId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comments_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [Interactions] (
        [Id] int NOT NULL IDENTITY,
        [EmojiId] int NOT NULL,
        [PostId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Interactions] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Interactions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Interactions_Emojis_EmojiId] FOREIGN KEY ([EmojiId]) REFERENCES [Emojis] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Interactions_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [MessageReadStatuses] (
        [Id] int NOT NULL IDENTITY,
        [MessageId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [IsSeen] bit NOT NULL,
        [ReadAt] datetime2 NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_MessageReadStatuses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MessageReadStatuses_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MessageReadStatuses_Messages_MessageId] FOREIGN KEY ([MessageId]) REFERENCES [Messages] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE TABLE [GroupMessageRecipients] (
        [Id] int NOT NULL IDENTITY,
        [GroupMessageId] int NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [IsRead] bit NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_GroupMessageRecipients] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_GroupMessageRecipients_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_GroupMessageRecipients_GroupMessage_GroupMessageId] FOREIGN KEY ([GroupMessageId]) REFERENCES [GroupMessage] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Comments_PostId] ON [Comments] ([PostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Conversations_User1Id] ON [Conversations] ([User1Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Conversations_User2Id] ON [Conversations] ([User2Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Friendships_ReceiverId] ON [Friendships] ([ReceiverId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Friendships_SenderId] ON [Friendships] ([SenderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_GroupMembers_GroupId] ON [GroupMembers] ([GroupId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_GroupMembers_UserId] ON [GroupMembers] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_GroupMessage_GroupId] ON [GroupMessage] ([GroupId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_GroupMessage_UserId] ON [GroupMessage] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_GroupMessageRecipients_GroupMessageId] ON [GroupMessageRecipients] ([GroupMessageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_GroupMessageRecipients_UserId] ON [GroupMessageRecipients] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Interactions_EmojiId] ON [Interactions] ([EmojiId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Interactions_PostId] ON [Interactions] ([PostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Interactions_UserId] ON [Interactions] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_MessageReadStatuses_MessageId] ON [MessageReadStatuses] ([MessageId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_MessageReadStatuses_UserId] ON [MessageReadStatuses] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Messages_ConversationId] ON [Messages] ([ConversationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Messages_ReceiverId] ON [Messages] ([ReceiverId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Messages_SenderId] ON [Messages] ([SenderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    CREATE INDEX [IX_Posts_UserId] ON [Posts] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240616132417_initialization'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240616132417_initialization', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617140719_addImagePost'
)
BEGIN
    CREATE TABLE [ImagePost] (
        [Id] int NOT NULL IDENTITY,
        [PostId] int NOT NULL,
        [Image] nvarchar(max) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_ImagePost] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ImagePost_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617140719_addImagePost'
)
BEGIN
    CREATE INDEX [IX_ImagePost_PostId] ON [ImagePost] ([PostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240617140719_addImagePost'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240617140719_addImagePost', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240624184421_editFriendShip'
)
BEGIN
    ALTER TABLE [Friendships] DROP CONSTRAINT [FK_Friendships_AspNetUsers_ReceiverId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240624184421_editFriendShip'
)
BEGIN
    ALTER TABLE [Friendships] DROP CONSTRAINT [FK_Friendships_AspNetUsers_SenderId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240624184421_editFriendShip'
)
BEGIN
    ALTER TABLE [Friendships] ADD CONSTRAINT [FK_Friendships_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240624184421_editFriendShip'
)
BEGIN
    ALTER TABLE [Friendships] ADD CONSTRAINT [FK_Friendships_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240624184421_editFriendShip'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240624184421_editFriendShip', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240628124010_editEntityUser'
)
BEGIN
    ALTER TABLE [Interactions] DROP CONSTRAINT [FK_Interactions_Emojis_EmojiId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240628124010_editEntityUser'
)
BEGIN
    DROP TABLE [Emojis];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240628124010_editEntityUser'
)
BEGIN
    DROP INDEX [IX_Interactions_EmojiId] ON [Interactions];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240628124010_editEntityUser'
)
BEGIN
    ALTER TABLE [Interactions] ADD [Emoji] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240628124010_editEntityUser'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Gender] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240628124010_editEntityUser'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Profile] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240628124010_editEntityUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240628124010_editEntityUser', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240629131701_addNotification'
)
BEGIN
    CREATE TABLE [Notifications] (
        [Id] int NOT NULL IDENTITY,
        [Content] nvarchar(max) NOT NULL,
        [PostId] int NULL,
        [IsSeen] bit NOT NULL,
        [SenderId] nvarchar(450) NOT NULL,
        [ReceiverId] nvarchar(450) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Notifications_AspNetUsers_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Notifications_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Notifications_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240629131701_addNotification'
)
BEGIN
    CREATE INDEX [IX_Notifications_PostId] ON [Notifications] ([PostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240629131701_addNotification'
)
BEGIN
    CREATE INDEX [IX_Notifications_ReceiverId] ON [Notifications] ([ReceiverId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240629131701_addNotification'
)
BEGIN
    CREATE INDEX [IX_Notifications_SenderId] ON [Notifications] ([SenderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240629131701_addNotification'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240629131701_addNotification', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240629131810_editNotification'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Notifications]') AND [c].[name] = N'SenderId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Notifications] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Notifications] ALTER COLUMN [SenderId] nvarchar(450) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240629131810_editNotification'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240629131810_editNotification', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240630132737_ediMessage'
)
BEGIN
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Messages_Conversations_ConversationId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240630132737_ediMessage'
)
BEGIN
    DROP INDEX [IX_Messages_ConversationId] ON [Messages];
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Messages]') AND [c].[name] = N'ConversationId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Messages] DROP CONSTRAINT [' + @var1 + '];');
    EXEC(N'UPDATE [Messages] SET [ConversationId] = 0 WHERE [ConversationId] IS NULL');
    ALTER TABLE [Messages] ALTER COLUMN [ConversationId] int NOT NULL;
    ALTER TABLE [Messages] ADD DEFAULT 0 FOR [ConversationId];
    CREATE INDEX [IX_Messages_ConversationId] ON [Messages] ([ConversationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240630132737_ediMessage'
)
BEGIN
    ALTER TABLE [Messages] ADD CONSTRAINT [FK_Messages_Conversations_ConversationId] FOREIGN KEY ([ConversationId]) REFERENCES [Conversations] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240630132737_ediMessage'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240630132737_ediMessage', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701181831_addProfileUser'
)
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Profile');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [Profile];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701181831_addProfileUser'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Birthday] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701181831_addProfileUser'
)
BEGIN
    CREATE TABLE [InforUsers] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [Hobbies] nvarchar(max) NULL,
        [Education] nvarchar(max) NULL,
        [AboutMe] nvarchar(max) NULL,
        [WorkAndExperience] nvarchar(max) NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_InforUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_InforUsers_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701181831_addProfileUser'
)
BEGIN
    CREATE INDEX [IX_InforUsers_UserId] ON [InforUsers] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240701181831_addProfileUser'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240701181831_addProfileUser', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    ALTER TABLE [Conversations] DROP CONSTRAINT [FK_Conversations_AspNetUsers_User1Id];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    ALTER TABLE [Conversations] DROP CONSTRAINT [FK_Conversations_AspNetUsers_User2Id];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    DROP INDEX [IX_Conversations_User1Id] ON [Conversations];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    DROP INDEX [IX_Conversations_User2Id] ON [Conversations];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Conversations]') AND [c].[name] = N'User1Id');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Conversations] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Conversations] DROP COLUMN [User1Id];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Conversations]') AND [c].[name] = N'User2Id');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Conversations] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Conversations] DROP COLUMN [User2Id];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Image');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Image] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Birthday');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Birthday] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'Address');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [AspNetUsers] ALTER COLUMN [Address] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    ALTER TABLE [AspNetUsers] ADD [ImageBackground] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    CREATE TABLE [ConversationMember] (
        [Id] int NOT NULL IDENTITY,
        [ConversationId] int NOT NULL,
        [IsCreate] bit NOT NULL,
        [UserId] nvarchar(450) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_ConversationMember] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ConversationMember_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ConversationMember_Conversations_ConversationId] FOREIGN KEY ([ConversationId]) REFERENCES [Conversations] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    CREATE INDEX [IX_ConversationMember_ConversationId] ON [ConversationMember] ([ConversationId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    CREATE INDEX [IX_ConversationMember_UserId] ON [ConversationMember] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705135851_addConverstionMember'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705135851_addConverstionMember', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705141108_editMessage'
)
BEGIN
    ALTER TABLE [Messages] DROP CONSTRAINT [FK_Messages_AspNetUsers_ReceiverId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705141108_editMessage'
)
BEGIN
    DROP INDEX [IX_Messages_ReceiverId] ON [Messages];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705141108_editMessage'
)
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Messages]') AND [c].[name] = N'ReceiverId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Messages] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Messages] DROP COLUMN [ReceiverId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240705141108_editMessage'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240705141108_editMessage', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706182717_editGroup'
)
BEGIN
    ALTER TABLE [Posts] ADD [GroupId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706182717_editGroup'
)
BEGIN
    ALTER TABLE [Groups] ADD [Background] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706182717_editGroup'
)
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GroupMembers]') AND [c].[name] = N'JoinedAt');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [GroupMembers] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [GroupMembers] ALTER COLUMN [JoinedAt] datetime2 NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706182717_editGroup'
)
BEGIN
    ALTER TABLE [GroupMembers] ADD [Status] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706182717_editGroup'
)
BEGIN
    CREATE INDEX [IX_Posts_GroupId] ON [Posts] ([GroupId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706182717_editGroup'
)
BEGIN
    ALTER TABLE [Posts] ADD CONSTRAINT [FK_Posts_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706182717_editGroup'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240706182717_editGroup', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706183019_editGroupmember'
)
BEGIN
    ALTER TABLE [GroupMembers] ADD [IsCreate] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240706183019_editGroupmember'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240706183019_editGroupmember', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240717180109_updateAccount'
)
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Groups]') AND [c].[name] = N'Description');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Groups] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [Groups] ALTER COLUMN [Description] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240717180109_updateAccount'
)
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Groups]') AND [c].[name] = N'Background');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Groups] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [Groups] ALTER COLUMN [Background] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240717180109_updateAccount'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240717180109_updateAccount', N'8.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240724124952_addReplyComment'
)
BEGIN
    CREATE TABLE [ReplyComments] (
        [Id] int NOT NULL IDENTITY,
        [CommentId] int NOT NULL,
        [CommenterId] nvarchar(450) NOT NULL,
        [Content] nvarchar(max) NOT NULL,
        [ResponderId] nvarchar(450) NOT NULL,
        [Created] datetime2 NOT NULL,
        CONSTRAINT [PK_ReplyComments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ReplyComments_AspNetUsers_CommenterId] FOREIGN KEY ([CommenterId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_ReplyComments_AspNetUsers_ResponderId] FOREIGN KEY ([ResponderId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_ReplyComments_Comments_CommentId] FOREIGN KEY ([CommentId]) REFERENCES [Comments] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240724124952_addReplyComment'
)
BEGIN
    CREATE INDEX [IX_ReplyComments_CommenterId] ON [ReplyComments] ([CommenterId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240724124952_addReplyComment'
)
BEGIN
    CREATE INDEX [IX_ReplyComments_CommentId] ON [ReplyComments] ([CommentId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240724124952_addReplyComment'
)
BEGIN
    CREATE INDEX [IX_ReplyComments_ResponderId] ON [ReplyComments] ([ResponderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240724124952_addReplyComment'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240724124952_addReplyComment', N'8.0.3');
END;
GO

COMMIT;
GO


﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class new_userEntities : DbContext
    {
        public new_userEntities()
            : base("name=new_userEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<bk_article> bk_article { get; set; }
        public virtual DbSet<bk_article_classify> bk_article_classify { get; set; }
        public virtual DbSet<bk_article_labels> bk_article_labels { get; set; }
        public virtual DbSet<bk_classify> bk_classify { get; set; }
        public virtual DbSet<bk_comments> bk_comments { get; set; }
        public virtual DbSet<bk_labels> bk_labels { get; set; }
        public virtual DbSet<bk_messageroom> bk_messageroom { get; set; }
        public virtual DbSet<bk_user> bk_user { get; set; }
        public virtual DbSet<bk_user_friends> bk_user_friends { get; set; }
        public virtual DbSet<bk_usercookie> bk_usercookie { get; set; }
        public virtual DbSet<bk_game> bk_game { get; set; }
    }
}

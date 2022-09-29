﻿using BusinessObjects.Data;
using BusinessObjects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {

        private static MemberDAO instance = null;
        private static readonly object iLock = new object();
        public MemberDAO()
        {

        }

        public static MemberDAO Instance
        {
            get
            {
                lock (iLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public Member FindMemberByEmailPassword(string email, string password)
        {
            var p = new Member();
            try
            {
                using (var context = new AppDbContext())
                {
                    p = context.Members.SingleOrDefault(x => x.Email == email && x.Password == password);

                    if (p == null)
                    {
                        throw new Exception("Wrong password or username");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public void SaveMember(Member member)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Members.Add(member);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateMember(Member member)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    context.Entry<Member>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

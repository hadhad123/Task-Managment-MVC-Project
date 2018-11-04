using Autofac;
using Autofac.Integration.Mvc;
using Data;
using Data.Infrastructure;
using Data.Repositories;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace TaskManagment.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();

        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();


            //Repositories
            builder.RegisterType<ApplicationUserRepository>()
                   .As<IApplicationUserRepository>().InstancePerRequest();

            builder.RegisterType<IdentityRoleRepository>()
                  .As<IDentityRoleRepository>().InstancePerRequest();

            builder.RegisterType<TaskRepository>()
                    .As<ITaskRepository>().InstancePerRequest();

            builder.RegisterType<TaskStatusRepository>()
                   .As<ITaskStatusRepository>().InstancePerRequest();
           
            builder.RegisterType<CommentRepository>()
                  .As<ICommentRepository>().InstancePerRequest();

            // Services
            builder.RegisterType<ApplicationUserService>()
               .As<IApplicationUserService>().InstancePerRequest();

            builder.RegisterType<RoleService>()
              .As<IRoleService>().InstancePerRequest();

            builder.RegisterType<TaskService>()
              .As<ITaskService>().InstancePerRequest();

            builder.RegisterType<TaskStatusService>()
              .As<ITaskStatusService>().InstancePerRequest();
            
            builder.RegisterType<EncryptionService>()
                .As<IEncryptionService>().InstancePerRequest();

            builder.RegisterType<ReportService>()
          .As<IReportService>().InstancePerRequest();

            Autofac.IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
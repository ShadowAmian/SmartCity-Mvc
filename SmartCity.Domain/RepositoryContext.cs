using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain
{
    public class RepositoryContext : IRepositoryContext
    {

        private readonly ConnectionStringSettings _connectionSeting = ConfigurationManager.ConnectionStrings["ConnectionString"];
        /// <summary>
        /// 初始化操作对象
        /// </summary>
        public RepositoryContext()
        {
            InitConnection();
        }
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public IDbConnection Conn { private set;get;}
        /// <summary>
        /// 创建数据库操作对象
        /// </summary>
        public void InitConnection()
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(this._connectionSeting.ProviderName);
            this.Conn = dbfactory.CreateConnection();
            if (Conn != null) this.Conn.ConnectionString = this._connectionSeting.ConnectionString;
        }
    }
}

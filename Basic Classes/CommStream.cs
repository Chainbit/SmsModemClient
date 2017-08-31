using GsmComm.GsmCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsModemClient
{
    /// <summary>
    /// Класс соединения с устройством
    /// </summary>
    public class CommStream : IDisposable
    {
        SmsModemBlock comm;

        /// <summary>
        /// Инициализатор класса <see cref="CommStream"/>
        /// </summary>
        /// <param name="c">Блок с которым идет обмен данными</param>
        public CommStream(SmsModemBlock c)
        {
            comm = c;
            if (!comm.IsOpen()) comm.Open();            
        }


        public void Dispose()
        {
            try
            {
                comm.Close();
            }
            catch (Exception e)
            {
            }
        }
    }
}

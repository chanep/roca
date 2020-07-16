using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading;

namespace Cno.Roca.Core.Utils
{
    public class SemaphoreFactory
    {
        /// <summary>
        /// Crea un semaforo y le agrega acceso al grupo de Administrators
        /// </summary>
        /// <param name="initialCount"></param>
        /// <param name="maximumCount"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Semaphore CreateSemaphore(int initialCount, int maximumCount, string name)
        {
            string user = "Everyone";
            SemaphoreSecurity semSec = new SemaphoreSecurity();


            SemaphoreAccessRule rule = new SemaphoreAccessRule(
                user,
                SemaphoreRights.Synchronize | SemaphoreRights.Modify,
                AccessControlType.Allow);
            semSec.AddAccessRule(rule);

            rule = new SemaphoreAccessRule(
                user,
                SemaphoreRights.FullControl | SemaphoreRights.ChangePermissions,
                AccessControlType.Allow);
            semSec.AddAccessRule(rule);
            bool createdNew;
            Semaphore semaphore = new Semaphore(initialCount, maximumCount, name, out createdNew, semSec);
            return semaphore;
        }
    }
}

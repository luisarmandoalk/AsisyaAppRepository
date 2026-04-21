using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asisya.Application.BackgroundJobs
{
    public static class ProductJobQueue
    {
        private static readonly Queue<(Guid categoryId, Guid supplierId)> _jobs = new();

        public static void Enqueue(Guid categoryId, Guid supplierId)
        {
            _jobs.Enqueue((categoryId, supplierId));
        }

        public static (Guid categoryId, Guid supplierId)? Dequeue()
        {
            if (_jobs.Count == 0)
                return null;

            return _jobs.Dequeue();
        }

        public static bool HasJobs => _jobs.Count > 0;
    }
}

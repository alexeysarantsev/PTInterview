using FiboApp2.Application.Abstractions.Services;
using FiboApp2.Application.Exceptions;
using System.Collections.Concurrent;
using System.Numerics;

namespace FiboApp2.Application.Services
{
    internal sealed class FiboCalculator : IFiboCalculator
    {
        public FiboCalculator()
        {
            _fiboNext[0] = 1;
            _fiboPrevious[1] = 1;
        }

        private const int maxCacheSize = 2000;
        private const int cacheSize = 1000;

        private readonly ConcurrentDictionary<BigInteger, BigInteger> _fiboPrevious = new ConcurrentDictionary<BigInteger, BigInteger>();
        private readonly ConcurrentDictionary<BigInteger, BigInteger> _fiboNext = new ConcurrentDictionary<BigInteger, BigInteger>();
        private readonly ConcurrentDictionary<BigInteger, object> _locks = new ConcurrentDictionary<BigInteger, object>();

        private readonly ConcurrentQueue<BigInteger> _cachedItems = new ConcurrentQueue<BigInteger>();

        private readonly ReaderWriterLockSlim _readerWriterLockSlim = new ReaderWriterLockSlim();

        public BigInteger Next(BigInteger current)
        {
            if (_fiboNext.ContainsKey(current))
            {
                return _fiboNext[current];
            }

            var lockObject = _locks.GetOrAdd(current, current => new object());
            lock (lockObject)
            {
                if (_fiboNext.ContainsKey(current))
                {
                    return _fiboNext[current];
                }

                BigInteger next;
                if (_fiboPrevious.ContainsKey(current))
                {
                    next = current + _fiboPrevious[current];
                }
                else
                {
                    next = GetNextFromScratch(current);
                }

                SaveToCache(current, next);
                FitCache();
                return next;
            }
        }

        private void SaveToCache(BigInteger current, BigInteger next)
        {
            _readerWriterLockSlim.EnterReadLock();

            _fiboNext[current] = next;
            _fiboPrevious[next + current] = next;

            _cachedItems.Enqueue(current);

            _readerWriterLockSlim.ExitReadLock();
        }

        /// <summary>
        /// При необходимости удаляет самые старые рассчитанные числа из кэша
        /// </summary>
        private void FitCache()
        {
            if (_cachedItems.Count > maxCacheSize)
            {
                _readerWriterLockSlim.EnterWriteLock();

                while (_cachedItems.Count > cacheSize)
                {
                    _cachedItems.TryPeek(out var keyToRemove);
                    _cachedItems.TryDequeue(out _);
                    _locks.TryRemove(keyToRemove, out var _);
                    _fiboPrevious.TryRemove(keyToRemove, out var _);
                    _fiboNext.TryRemove(keyToRemove, out var _);

                }
                _readerWriterLockSlim.ExitWriteLock();
            }
        }

        /// <summary>
        /// Рассчет N+1 числа Фибоначчи 
        /// </summary>
        /// <param name="number">N число Фибоначчи </param>
        private BigInteger GetNextFromScratch(BigInteger number)
        {
            if (number == 0)
            {
                return 1;
            }
            if (number == 1)
            {
                return 1;
            }

            BigInteger current = 1;
            BigInteger prev = 1;

            while (current < number)
            {
                BigInteger next = current + prev;
                prev = current;
                current = next;
            }

            if (current != number)
            {
                throw new NotFiboNumberException("Not a Fibo number", number);
            }

            return current + prev;
        }
    }
}

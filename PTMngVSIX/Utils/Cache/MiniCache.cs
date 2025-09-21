using System;
using System.Threading.Tasks;

namespace PTMngVSIX.Utils.Cache
{
	internal class MiniCache<T>
	{
		private TimeSpan _expiration;
		private DateTime _expirationTime;
		private readonly Func<Task<T>> _factory;
		private T _value;
		private bool _initialized;

		public MiniCache(TimeSpan expiration, Func<Task<T>> factory)
		{
			_expiration = expiration;
			_expirationTime = DateTime.Now;
			_factory = factory;
		}

		public async Task<T> GetAsync()
		{
			if (DateTime.Now <= _expirationTime && _initialized)
			{
				return _value;
			}

			_value = await _factory();
			_initialized = true;
			_expirationTime = DateTime.Now.Add(_expiration);

			return _value;
		}
	}
}

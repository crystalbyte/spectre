using System;

namespace Crystalbyte.Chocolate
{
	public sealed class SchemeDescriptor
	{
		public SchemeDescriptor (string scheme, SchemeHandlerFactory factory)
			: this(scheme, string.Empty, factory) {	}

		public SchemeDescriptor (string scheme, string domain, SchemeHandlerFactory factory)
		{
			Scheme = scheme;
			Domain= domain;
			Factory = factory;
		}

		public string Scheme {
			get;
			private set;
		}

		public string Domain {
			get;
			private set;
		}

		public SchemeHandlerFactory Factory {
			get;
			private set;
		}
	}
}


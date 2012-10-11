namespace Crystalbyte.Spectre.Web{
    public sealed class SpectreSchemeDescriptor : ISchemeDescriptor{
        #region ISchemeDescriptor Members

        public string Scheme{
            get { return Schemes.Spectre; }
        }

        public SchemeProperties SchemeProperties{
            get{
                return SchemeProperties.Local
                       | SchemeProperties.DisplayIsolated
                       | SchemeProperties.Standard;
            }
        }

        #endregion
    }
}
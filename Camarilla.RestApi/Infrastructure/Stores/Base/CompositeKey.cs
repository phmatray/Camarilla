namespace Camarilla.RestApi.Infrastructure.Stores.Base
{
    public struct CompositeKey
    {
        public CompositeKey(int keyPersona, int keyMail)
        {
            KeyPersona = keyPersona;
            KeyMail = keyMail;
        }

        public int KeyPersona { get; }
        public int KeyMail { get; }
    }
}
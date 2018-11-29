namespace SignatureRepository
{
    public class SignatureFactory
    {
        public ISignaturesService GetSignatureService() {
            return new FileSignatureService();
        }
    }
}

using System.Runtime.Serialization;

namespace EcommerceAPI.Model.PurchaseOrder
{
    public enum StatusOrder
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value ="The payment was received")]
        PaymentReceived,
        [EnumMember(Value ="The payment had errors")]
        FailedPayment
    }
}

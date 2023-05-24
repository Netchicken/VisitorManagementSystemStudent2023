using VisitorManagementSystem.Enum;

namespace VisitorManagementSystem.Services
{
    public interface ISweetAlert
    {
        string AlertCancel(string title, string message, string buttonText, SweetAlertEnum.NotificationType notificationType);
        string AlertPopup(string title, string message, SweetAlertEnum.NotificationType notificationType);
        string AlertPopupWithImage(string title, string message, SweetAlertEnum.NotificationType notificationType);
    }
}
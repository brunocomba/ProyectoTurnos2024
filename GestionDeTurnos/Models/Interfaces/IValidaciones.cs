
namespace Models.Interfaces
{
    public interface IValidaciones
    {
        bool SoloLetras(string textBox);
        bool SoloNumeros(int textBox);
        bool SoloNumerosEnTel(uint textBox);
        bool SoloNumerosEnPrecio(decimal textBox);
        bool TelCompleto(uint Tel);
        bool DniCompleto(int DNI);
        bool CumpleRequisitosEmail(string Pass);
        bool CumpleRequisitosPass(string Pass);
        bool ConfirmarPass(string Pass, string ConfirPass);
        bool Mayor18(DateTime nacimiento);
        
    }

}

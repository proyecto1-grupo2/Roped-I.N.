/*Estados del gancho:
    Quieto: Cuando el gancho no realiza ninguna acción, está en reposo
    Ida: Cuando el gancho está avanza hasta alcanzar su rango máximo o chocar contra algún objeto 
    Vuelta: Cuando el gancho vuelve hacia el jugador
    Enganchado: cuando el gancho permanece en una zona en la que el jugador puede engancharse 
    */
public enum HookState
{
    Quieto, Ida, Vuelta, Enganchado
}

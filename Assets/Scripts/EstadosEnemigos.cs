/*Estados del gancho:
    -Quemado: Cuando el enemigo ha sido impactado con el quimico de fuego, durante 3 segundos pierde vida
    -Congelado: Cuando el ha sido impactado con el quimico de hielo, 
    durante 3 segundos no se puede mover y no hace daño al jugador
    -Paralizado: Cuando el enemigo ha sido impactado con el quimico electrico, 
    durante 2 segundos no se puede mover pero si realizar daño al jugador
    Nada: Estado base del enemigo
    */
public enum EnemyState
{
    Nada, Quemado, Congelado, Paralizado
}

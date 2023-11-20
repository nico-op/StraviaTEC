export interface Grupo {
    nombreGrupo: string;
    descripcion: string;
    administrador: string;
    creacion?: Date; // El signo de interrogación indica que esta propiedad es opcional (puede ser nula)
    grupoId: string;
}

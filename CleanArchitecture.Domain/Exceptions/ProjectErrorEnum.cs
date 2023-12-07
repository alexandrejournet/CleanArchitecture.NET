namespace CleanArchitecture.Domain.Exceptions
{
    public enum ProjectErrorEnum
    {
        PROJECT_400,
        PROJECT_400_WITH_CAUSE,
        PROJECT_401,
        PROJECT_401_WITH_CAUSE,
        PROJECT_402,
        PROJECT_403,
        PROJECT_403_WITH_CAUSE,
        PROJECT_404,
        PROJECT_404_WITH_CAUSE,
        PROJECT_404_YEARS_YOUNGS_ENSEMBLE_NOT_FOUND,
        PROJECT_404_YOUNG_NOT_FOUND,
        PROJECT_404_OCCURRENCE_NOT_FOUND,
        PROJECT_404_ENSEMBLE_NOT_FOUND,
        PROJECT_405,
        PROJECT_406,
        PROJECT_406_OS_NOT_FOUND,
        PROJECT_406_OS_MISSING,
        PROJECT_406_VERSION_ERROR,
        PROJECT_406_STATUT_OCCURRENCE_NOT_ACCEPTABLE,
        PROJECT_407,
        PROJECT_408,
        PROJECT_409,
        PROJECT_410,
        PROJECT_500,
        PROJECT_500_WITH_CAUSE,
        PROJECT_500_CANT_GET_ENTITIES,
        PROJECT_500_OCCURRENCE_PARTICIPANT_NOT_VALIDE,
        PROJECT_422,
        PROJECT_422_WITH_CAUSE,
    }

    public static class ProjectErrorEnumExtension
    {
        public static string ShowError(this ProjectErrorEnum ProjectErrorEnum)
        {
            return ProjectErrorEnum switch
            {
                ProjectErrorEnum.PROJECT_400 => "Le paramètre est erroné.",
                ProjectErrorEnum.PROJECT_400_WITH_CAUSE => "Le paramètre est erroné : {0}",
                ProjectErrorEnum.PROJECT_401 => "Accès non autorisé.",
                ProjectErrorEnum.PROJECT_401_WITH_CAUSE => "Accès non autorisé : {0}",
                ProjectErrorEnum.PROJECT_403 => "Accès interdit",
                ProjectErrorEnum.PROJECT_403_WITH_CAUSE => "Accès interdit : {0}",
                ProjectErrorEnum.PROJECT_404 => "Aucun résultat trouvé.",
                ProjectErrorEnum.PROJECT_404_WITH_CAUSE => "Aucun résultat trouvé: {0}",
                ProjectErrorEnum.PROJECT_404_ENSEMBLE_NOT_FOUND => "L'ensemble n'existe pas en base de données: {0}",
                ProjectErrorEnum.PROJECT_404_YEARS_YOUNGS_ENSEMBLE_NOT_FOUND => "Aucun jeune sur cet ensemble: {0}",
                ProjectErrorEnum.PROJECT_404_YOUNG_NOT_FOUND => "Ce jeune n'existe pas: {0}",
                ProjectErrorEnum.PROJECT_404_OCCURRENCE_NOT_FOUND => "L'occurrence n'existe pas: {0}",
                ProjectErrorEnum.PROJECT_406_STATUT_OCCURRENCE_NOT_ACCEPTABLE => "Le statut de l'occurrence n'autorise pas la modification.",
                ProjectErrorEnum.PROJECT_406_OS_MISSING => "Le système d'exploitation est manquant: {0} - {1}",
                ProjectErrorEnum.PROJECT_406_OS_NOT_FOUND => "Le système d'exploitation est inconnue de notre base de données: {0} - {1}",
                ProjectErrorEnum.PROJECT_422 => "",
                ProjectErrorEnum.PROJECT_422_WITH_CAUSE => "Erreur de validation : {0}",
                ProjectErrorEnum.PROJECT_500_CANT_GET_ENTITIES => "Impossible de récupérer les entités: {0}",
                ProjectErrorEnum.PROJECT_500_OCCURRENCE_PARTICIPANT_NOT_VALIDE => "Le nombre de participant est invalide.",
                ProjectErrorEnum.PROJECT_500 => "Une erreur serveur est survenue.",
                ProjectErrorEnum.PROJECT_500_WITH_CAUSE => "Une erreur serveur est survenue : {0}",
                _ => "",
            };
        }
    }
}
export class TestTypeSettings {
    testTypeId: number;
    startWithoutConfirmation: boolean;
    startBeforeProgrammation: boolean;
    startAfterProgrammation: boolean;
    possibleGetToSkipped: boolean;
    possibleChangeAnswer: boolean;
    canViewResultWithoutVerification: boolean;
    canViewPollProgress?: boolean;
    hidePagination?: boolean;
    showManyQuestionPerPage?: boolean;
    questionsCountPerPage?: number;
    maxErrors?: number;
}

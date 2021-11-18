export interface AuthModel {
    isAuthenticated: boolean;
    user: {
        name: string;
        email: string;
        avatar: string;
    },
    availableModules: {
        id: number;
        name: string;
        publicUrl: string;
        icon: string;
    }
}

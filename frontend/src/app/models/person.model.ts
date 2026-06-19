export interface SocialAccount {
  type: string;
  address: string;
}

export interface PersonRequest {
  firstName: string;
  lastName: string;
  socialSkills: string[];
  socialAccounts: SocialAccount[];
}

export interface PersonResponse {
  vowelCount: number;
  consonantCount: number;
  fullName: string;
  reversedFullName: string;
  person: {
    firstName: string;
    lastName: string;
    socialSkills: string[];
    socialAccounts: SocialAccount[];
  };
}
export enum Platform {
  Steam = 'Steam',
  EpicGames = 'Epic Games',
  Xbox = 'Xbox',
  PlayStation = 'PlayStation',
}

export const platformsIcons = (platform: string) => {
  switch (platform) {
    case Platform.Steam:
      return `assets/icons/platforms/steam-icon.svg`;
    case Platform.PlayStation:
      return `assets/icons/platforms/play-station-icon.svg`;
    case Platform.Xbox:
      return `assets/icons/platforms/xbox-icon.svg`;
    case Platform.EpicGames:
      return `assets/icons/platforms/epic-games-icom.svg`;
    default:
      return '';
  }
};

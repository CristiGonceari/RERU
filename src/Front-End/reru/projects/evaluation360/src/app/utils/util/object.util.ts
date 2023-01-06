export class ObjectUtil {
  public static clone(object: any) {
    return JSON.parse(JSON.stringify(object));
  }

  public static preParseObject(object): any {
    for (const key in object) {
      if (!object[key]) {
        delete object[key];
      }
    }

    return object;
  }

  public static hasMatchEvent(data): boolean {
    return data && data.match && typeof data.match === 'function';
  }

  public static isDateStruct(data): boolean {
    return data && data.year && data.month && !!data.day;
  }

  public static capitalizeProperties(object): any {
    for(let prop in object) {
      object[prop.charAt(0).toUpperCase() + prop.slice(1)] = object[prop];
      delete object[prop];
    }

    return object;
  }
}

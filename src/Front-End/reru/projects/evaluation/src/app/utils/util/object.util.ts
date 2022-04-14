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
  }
  
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
  }
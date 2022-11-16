export class ObjectUtil {
    public static clone(object: any) {
      return JSON.parse(JSON.stringify(object));
    }
  
    /**
     * Cleans the object containing falsy data
     * 
     * @param {Object} object contains list of parameters/query params/body data
     * @returns {Object} clean parameters for that particular object
     */
    public static preParseObject(object: any): any {
      for (const key in object) {
        if (!object[key] && typeof object[key] !== 'boolean' || 
            isNaN(object[key]) && typeof object[key] === 'number') {

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
  
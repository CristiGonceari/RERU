import { SUCCESS_NOTIFICATION_TIME, MID_NOTIFICATION_TIME, FAILED_NOTIFICATION_TIME } from "../constants/notifcation.constant";

export class NotificationUtil {
  public static getDefaultConfig() {
    return {
      animate: 'fromTop',
      position: ['top', 'right'],
      timeOut: SUCCESS_NOTIFICATION_TIME,
      lastOnBottom: true,
      showProgressBar: true
    };
  }

  public static getDefaultMidConfig() {
    return {
      animate: 'fromTop',
      position: ['top', 'right'],
      timeOut: MID_NOTIFICATION_TIME,
      lastOnBottom: true,
      showProgressBar: true
    };
  }

  public static getWarnConfig() {
    return {
      animate: 'fromTop',
      position: ['top', 'right'],
      timeOut: FAILED_NOTIFICATION_TIME,
      lastOnBottom: true,
      showProgressBar: true
    };
  }
}

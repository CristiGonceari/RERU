import { AddressModel, BulletinModel } from '../../../utils/models/bulletin.model';
import { Contractor } from '../../../utils/models/contractor.model';
import { StudyModel } from '../../../utils/models/study.model';
import { ObjectUtil } from '../../../utils/util/object.util';
import { DocumentTypeEnum } from '../../../utils/models/document-type.enum';
import {  InstructionModel } from 'projects/personal/src/app/utils/models/contract.model';

export class ContractorParser {
    constructor() {}

    public static parseContractor(data: Contractor): Contractor {
        return ObjectUtil.preParseObject({
            id: data.id,
            firstName: data.firstName,
            lastName: data.lastName,
            fatherName: data.fatherName,
            homePhone: data.homePhone,
            phoneNumber: data.phoneNumber,
            workPhone: data.workPhone,
            candidateNationalityId: data.candidateNationalityId,
            candidateCitizenshipId: data.candidateCitizenshipId,
            stateLanguageLevel: data.stateLanguageLevel,
            birthDate: data.birthDate,
            bloodTypeId: data.bloodTypeId ? +data.bloodTypeId : null,
            sex: data.sex ? +data.sex : null,
        })
    }

    public static parseBulletin(data: BulletinModel): BulletinModel {
        return ObjectUtil.preParseObject({
            id: data.id,
            series: data.series,
            releaseDay: data.releaseDay,
            emittedBy: data.emittedBy,
            idnp: data.idnp,
            birthPlace: data.birthPlace,
            livingAddress: data.livingAddress,
            residenceAddress: data.residenceAddress,
            contractorId: data.contractorId
        })
    }

    public static parseInstruction(data: InstructionModel): InstructionModel {
      return ObjectUtil.preParseObject({
          id: data.id,
          thematic: data.thematic,
          instructorName: data.instructorName,
          instructorLastName: data.instructorLastName,
          duration: data.duration,
          date: data.date,
          contractorId: data.contractorId,
      })
  }


    public static parseStudy(data: StudyModel, contractorId): StudyModel {
        return ObjectUtil.preParseObject({
          id: data.id,
          institution: data.institution,
          studyFrequency: data.studyFrequency,
          studyTypeId: data.studyTypeId,
          institutionAddress: data.institutionAddress,
          faculty: data.faculty,
          specialty: data.specialty,
          yearOfAdmission: data.yearOfAdmission,
          graduationYear: data.graduationYear,
          contractorId: contractorId,
          studyCourse: data.studyCourse,
          startStudyPeriod: data.startStudyPeriod,
          endStudyPeriod: data.endStudyPeriod,
          title: data.title,
          studyProfile: data.studyProfile,
          qualification: data.qualification,
          creditCount: data.creditCount,
          studyActSeries: data.studyActSeries,
          studyActNumber: data.studyActNumber,
          studyActRelaseDay: data.studyActRelaseDay
        })
    }

    public static parseStudies(data: StudyModel[], contractorId: number): StudyModel[] {
        return data.map(el => this.parseStudy(el, contractorId));
    }

    public static parseFile(data, id: number, type: DocumentTypeEnum ): FormData {
        const request = new FormData();
        request.append('contractorId', `${id}`);
        request.append('File', data.file || data);
        request.append('Type', `${type}`); 
        return request;
    }

    public static renderAddressOrder(data: AddressModel): string {
        if (this.hasCountryOnly(data)) {
          return data.country;
        }
    
        if (!this.hasAddressData(data)) {
          return '-';
        }
    
        if (data.region) {
          return `${data.country || ''}, ${data.region ? data.region + ',' : ''} ${data.city ? 'or. ' + data.city + ',' : ''} ${data.street ? 'str ' + data.street + ',' : ''} ${data.building ? 'bl. ' + data.building + ',' : ''} ${data.apartment ? 'ap. ' + data.apartment : ''}`.trim().replace(/(^\,)|(\,$)/g, '');
        }

        return `${data.country || ''}, ${data.city ? 'or. ' + data.city + ',' : ''} ${data.street ? 'str ' +data.street + ',' : ''} ${data.building ? 'bl. ' + data.building + ',' : ''} ${data.apartment ? 'ap. ' + data.apartment : ''}`.trim().replace(/(^\,)|(\,$)/g, '')
      }
    
      private static hasAddressData(data: AddressModel): boolean {
        for(let prop in data) {
          if (data[prop]) return true;
        }
    
        return false;
      }
    
      private static hasCountryOnly(data: AddressModel): boolean {
        if (!data.city && !data.street && !data.building && !data.apartment && data.country) {
          return true;
        }
    
        return false;
      }
}

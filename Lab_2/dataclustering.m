
%------------ Applying K-means Algortihm in Data Clustering --------------%
%---------------- By Y.YELLA REDDY, BTech IIT BHUBANESWAR ----------------%

clc;

%--------------- Initializing the dataset of flowers ---------------------%
load irisdataset.txt
DataSet=irisdataset;

Dim=size(DataSet);

%----------------------- Selecting 3 random centres ----------------------%
Selection=rand(1,3);
Selection=Selection*Dim(1,1);
Selection=ceil(Selection); %Selecting the row number.

%--------------------------- 3 random centres ----------------------------%
Centre1=DataSet(Selection(1),:);
Centre2=DataSet(Selection(2),:);
Centre3=DataSet(Selection(3),:);

n=input('No Of Iterations : ')
%----------------------- Partitional Algorithm ---------------------------%
%-------------------------- K means Algorithm ----------------------------%
% for j=1:1:n
%     count1=0;
%     Mean1=zeros(1,4);
%     count2=0;
%     group1=[];
%     Mean2=zeros(1,4);
%     group2=[];
%     count3=0;
%     group3=[];
%     Mean3=zeros(1,4);
%     
%     %Finding the minimum distance
%     for i=1:1:Dim(1,1)       
%         Pattern1(i)=sqrt((Centre1(1,1)-DataSet(i,1))^2+(Centre1(1,2)-DataSet(i,2))^2+(Centre1(1,3)-DataSet(i,3))^2+(Centre1(1,4)-DataSet(i,4))^2);
%         Pattern2(i)=sqrt((Centre2(1,1)-DataSet(i,1))^2+(Centre2(1,2)-DataSet(i,2))^2+(Centre2(1,3)-DataSet(i,3))^2+(Centre1(1,4)-DataSet(i,4))^2);
%         Pattern3(i)=sqrt((Centre3(1,1)-DataSet(i,1))^2+(Centre3(1,2)-DataSet(i,2))^2+(Centre3(1,3)-DataSet(i,3))^2+(Centre1(1,4)-DataSet(i,4))^2);
%         
%         LessDist=[Pattern1(i) Pattern2(i) Pattern3(i)];
%         Minimum=min(LessDist);
%         
%         %Finding the new centre
%         if (Minimum==Pattern1(i))
%             count1=count1+1;
%             Mean1=Mean1+DataSet(i,:);
%             group1=[group1 i];
%         else if (Minimum==Pattern2(i))
%                 count2=count2+1;
%                 Mean2=Mean2+DataSet(i,:);
%                 group2=[group2 i];
%             else count3=count3+1;
%                 Mean3=Mean3+DataSet(i,:);
%                 group3=[group3 i];
%             end
%         end        
%     end
%     
%     %----------------------------- New Centres -------------------------------%
%     Centre1=Mean1/count1;
%     Centre2=Mean2/count2;
%     Centre3=Mean3/count3;
% %     plot(j,count1,'r');
% %     hold on
% %     plot(j,count2,'g');
% %     plot(j,count3,'b');
%     
% end

[IDX,C] = kmeans(DataSet,3);

hold off
% specify the indexed color for each point
%icolor = ceil((DataSet(:,3)/max(DataSet(:,3)))*256);

%figure,
%scatter3(DataSet(:,1),DataSet(:,2),DataSet(:,3),DataSet(:,3),icolor,'filled');
%figure,

%scatter3(DataSet(:,1),DataSet(:,2),DataSet(:,3),[],DataSet(:,4),'filled');
figure
scatter3(DataSet(:,1),DataSet(:,2),DataSet(:,3), 40, IDX, 'filled')
xlabel SL, ylabel SW, zlabel PL


X = [IDX(1,:),IDX(2,:),IDX(3,:),IDX(4,:),IDX(5,:)];
varNames = {'1Param'; '2Param'; '3Param'; '4Param'; '5Param'};
figure
plotmatrix(X);
text([.08 .24 .43 .66 .83], repmat(-.1,1,5), varNames, 'FontSize',8);
text(repmat(-.12,1,5), [.86 .62 .41 .25 .02], varNames, 'FontSize',8, 'Rotation',90);

